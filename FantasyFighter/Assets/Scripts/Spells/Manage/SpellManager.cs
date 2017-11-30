using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Specialized;
using DuloGames.UI;

public class SpellManager : MonoBehaviour {
    public static SpellManager SM;
	// private string selectedSpell;

    public GameObject projectileMeteor;
    public GameObject projectileSnowball;
    public GameObject projectileFire;

    public CastBar castBar;

    [HideInInspector]
    public Sprite[] spellIcons;
    public Sprite[] spellIconsGrey;

    public StringDictionary spellDescriptions = new StringDictionary();

    private GameObject projectile;

    public List<GameObject> worldSpells = new List<GameObject>();

    private void Awake() {
        MakeThisTheOnlySpellManager();
        InitSpellIcons();
        InitDictionary();
    }

    void MakeThisTheOnlySpellManager() {
        if (SM == null) {
            DontDestroyOnLoad(gameObject);
            SM = this;
        } else {
            if (SM != this) {
                Destroy(gameObject);
            }
        }
    }

    private void InitSpellIcons() {
        spellIcons = new Sprite[160];
        spellIconsGrey = new Sprite[160];

        for (int x = 1; x < 159; x++) {
            spellIcons[x] = Resources.Load<Sprite>("FantasySpellsIconPack/Colored/icon_128x128_" + x);
            spellIconsGrey[x] = Resources.Load<Sprite>("FantasySpellsIconPack/Grey/icon_128x128_" + x + "_Grey");
        }
    }

    private void InitDictionary() {
        spellDescriptions.Add("Meteor","Fires a meteor that explodes");
        spellDescriptions.Add("Fire Nova","Fake fire nova");
        spellDescriptions.Add("Snowball","Shoots a snowball that explodes");
        spellDescriptions.Add("AcidArrow","Description needed");
        spellDescriptions.Add("Blink","Description needed");
        spellDescriptions.Add("FireNova","Description needed");
        spellDescriptions.Add("FrostBlast","Description needed");
        spellDescriptions.Add("PlagueBlast","Description needed");
        spellDescriptions.Add("Swap","Description needed");
    }

    public GameObject AddWorldSpell(GameObject worldSpell) {
        GameObject currentSpell = Instantiate(worldSpell);

        GameManager.GM.objectUIScript.AddSpellUI(currentSpell);
        worldSpells.Add(currentSpell);

        return currentSpell;
    }

    public void RemoveWorldSpell(GameObject worldSpell) {
        GameManager.GM.objectUIScript.RemoveSpellUI(worldSpell);
        worldSpells.Remove(worldSpell);
        Destroy(worldSpell);
    }

    public bool SelectSpell(int spellNum) {
		var spell = GameManager.GM.currentPlayer.spells[spellNum];
		if (spell == null) {
            ConsoleProDebug.LogToFilter("No spell selected", "Spell");
            return false;
        }

        UnitScript unitScript =  GameManager.GM.currentPlayer.unit.GetComponent<UnitScript>();
        if (unitScript.isDead) {
            ConsoleProDebug.LogToFilter("Unit is dead", "Spell");
            return false;
        }
        if (unitScript.currentMana < spell.Info.PowerCost) {
            ConsoleProDebug.LogToFilter("Unit is dead", "Spell");
            return false;
        }
        
        // Success
        unitScript.selectedSpell = spell;
        unitScript.selectedSpellIdx = spellNum;

        castBar.SetSpell(spell);
        if (spell.channelCounter > 0f) {
            castBar.Show();
        } else {
            castBar.HideInstant();
        }
        return true;
	}

	public void CastSpellMouse(int ownerNum, Spell castSpell, Vector3 clickPos) {
        Player owner = GameManager.GM.players[ownerNum];

        owner.unit.GetComponent<UnitScript>().selectedSpell = null;           
        owner.spells[owner.unit.GetComponent<UnitScript>().selectedSpellIdx] = null;
        owner.unit.GetComponent<UnitScript>().selectedSpellIdx = 0;

        owner.unit.GetComponent<UnitScript>().currentMana -= castSpell.Info.PowerCost;
        // CastSpell(SpellFireNova);
        StartCoroutine(SpellDelay(0.2f, owner, castSpell.Info.Name, clickPos));
	}

    public void ReleaseSpell(Spell spell) {
        spell.SpellObject = Instantiate(spell.Info.spellObject);
        spell.SpellScript = spell.SpellObject.GetComponent<SpellObject>();
        spell.SpellScript.Init(spell, Vector3.zero);
    }
	
	private IEnumerator SpellDelay(float delay, Player owner, string spellName, Vector3 clickPos) {
		yield return new WaitForSeconds(delay);

        switch (spellName) {
            case "Meteor":
                createProjectile(projectileMeteor, owner, clickPos);
                break;
            case "Fire Nova":
                createProjectile(projectileFire, owner, owner.unit.GetComponent<UnitScript>().transform.position);
                break;
            case "Glacial Blast":
                createProjectile(projectileSnowball, owner, clickPos);
                break;
        }
	}

    private void createProjectile(GameObject projectile, Player owner, Vector3 destination) {
		GameObject mProjectile = Instantiate(projectile);
		mProjectile.transform.position = owner.unit.transform.position;
		mProjectile.transform.LookAt(destination);

        // You dont know what methods the instantiated class will contain
        // SendMessage(string methodName, object value)
        mProjectile.SendMessage("setOwner", owner);
    }
}

/*
Spell Variables:
- Range
- Cooldown
- Cast time
- Cast type 
    Charge - become stronger the longer you charge it
    Channel - finish charging before cast
    Instant - charge finishes instantly
- Hold time
- Amount
    Amount of times the spell can be cast. i.e. You could get 5 fireballs in 1 pickup. A lazer beam will have 150 amount and that will decrease with each second of use.
- Power cost
- Damage

Positioning:
- Straight projectile (acid arrow)
    Launch a projectile in a direction
- AoE (fire nova)
    Apply spell to everyone around casting po
- AoE projectile (plague blast / cold blast)
    Launch a projectile 
- Area / Position (blink)
    Cast spell in a target area at a select position
- Target (swap)
    Target a Unit / Tree / House / Object that is within range, and cast it

SPELL IDEAS:
Electro ball
- Launch a slow moving projectile that shocks enemies if they get too close

Lazer beam
- Straight forward lazer beam that allows you to shoot at target for X seconds

Piercing arrow
- Shoots out an arrow. The arrow travels faster and further the longer its charged
*/