using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Specialized;

public class SpellManager : MonoBehaviour {
    public static SpellManager SM;
	private string selectedSpell;

    public GameObject projectileMeteor;
    public GameObject projectileSnowball;

    public Spell[] spells;

    [HideInInspector]
    public Sprite[] spellIcons;
    public Sprite[] spellIconsGrey;

    public StringDictionary spellDescriptions = new StringDictionary();

    private GameObject projectile;

    // private int worldSpellCount = 0;
    // public GameObject[] worldSpells;

    public List<GameObject> worldSpells = new List<GameObject>();


    private void Awake() {
        MakeThisTheOnlySpellManager();
        InitSpellIcons();
        InitDictionary();
        InitSpells();
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

    // Type: charge | channel | instant
    private void InitSpells() {
        spells = new Spell[] {
            new Spell(
                0, 
                "Meteor", 
                spellIcons[40],  
                50f,
                10f, 
                3f, 
                30f, 
                projectileMeteor, 
                spellDescriptions["Meteor"],
                20f,
                2f,
                "channel",
                3
                ),
                
            new Spell(
                1,
                "Fire Nova",
                spellIcons[41],
                5f,
                10f,
                3f,
                30f,
                null,
                spellDescriptions["Fire Nova"],
                20f,
                2f,
                "instant",
                1
                ),

            new Spell(
                2,
                "Snowball",
                spellIcons[1],
                25f,
                10f,
                3f,
                30f,
                projectileSnowball,
                spellDescriptions["Snowball"],
                20f,
                2f,
                "channel",
                1
             ),
            new Spell(
                3,
                "AcidArrow",
                spellIcons[33],
                25f,
                10f,
                2f,
                20f,
                projectileSnowball,
                spellDescriptions["AcidArrow"],
                20f,
                2f,
                "charge",
                2
             ),
            new Spell(
                4,
                "Blink",
                spellIcons[22],
                25f,
                10f,
                2f,
                20f,
                projectileSnowball,
                spellDescriptions["Blink"],
                20f,
                2f,
                "instant",
                3
             ),

            new Spell(
                5,
                "FireNova",
                spellIcons[41],
                25f,
                10f,
                2f,
                20f,
                projectileSnowball,
                spellDescriptions["FireNova"],
                20f,
                2f,
                "instant",
                2
             ),

            new Spell(
                6,
                "FrostBlast",
                spellIcons[2],
                25f,
                10f,
                2f,
                20f,
                projectileSnowball,
                spellDescriptions["FrostBlast"],
                20f,
                2f,
                "channel",
                1
             ),

            new Spell(
                7,
                "PlagueBlast",
                spellIcons[77],
                25f,
                10f,
                2f,
                20f,
                projectileSnowball,
                spellDescriptions["PlagueBlast"],
                20f,
                2f,
                "channel",
                1
             ),

            new Spell(
                8,
                "Swap",
                spellIcons[33],
                25f,
                10f,
                2f,
                20f,
                projectileSnowball,
                spellDescriptions["Swap"],
                20f,
                2f,
                "channel",
                2
             )
         };
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


    public Spell getSpell(string spellName) {
        for (int x = 0; x < spells.Length; x++) {
            if (spells[x].Name == spellName) {
                return spells[x];
            }
        }
        Debug.Log("spellName does not exist");
        return null;
    }

    public void SelectSpell(int spellNum) {
		var spell = GameManager.GM.currentPlayer.spells[spellNum];
		if (spell == null) {
            Debug.Log("No spell selected");
            return;
        }

        UnitScript unitScript =  GameManager.GM.currentPlayer.unit.GetComponent<UnitScript>();
        if (unitScript.isDead) {
            Debug.Log("Unit is dead");
            return;
        }
        if (unitScript.currentMana < spell.PowerCost) {
            Debug.Log("Not enough mana");
            return;
        }
        
        // Success
        unitScript.selectedSpell = spell;
        unitScript.selectedSpellIdx = spellNum;
	}

	public void CastSpellMouse(int ownerNum, Spell castSpell, Vector3 clickPos) {
        Player owner = GameManager.GM.players[ownerNum];

        owner.unit.GetComponent<UnitScript>().selectedSpell = null;           
        owner.spells[owner.unit.GetComponent<UnitScript>().selectedSpellIdx] = null;
        owner.unit.GetComponent<UnitScript>().selectedSpellIdx = 0;

        owner.unit.GetComponent<UnitScript>().currentMana -= castSpell.PowerCost;

        StartCoroutine(SpellDelay(0.2f, owner, castSpell.Name, clickPos));
	}
	
	private IEnumerator SpellDelay(float delay, Player owner, string spellName, Vector3 clickPos) {
		yield return new WaitForSeconds(delay);

        switch (spellName) {
            case "Meteor":
                createProjectile(projectileMeteor, owner, clickPos);
                break;
            case "Snowball":
                createProjectile(projectileSnowball, owner, clickPos);
                break;
        }
	}

    private void createProjectile(GameObject projectile, Player owner, Vector3 destination) {
		GameObject mProjectile = Instantiate(projectile);
		mProjectile.transform.position = owner.unit.transform.position;
		mProjectile.transform.LookAt(destination);

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