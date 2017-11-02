using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour {
    public static SpellManager SM;
	private string selectedSpell;

    public GameObject projectileMeteor;
    public GameObject projectileSnowball;

    public Spell[] spells;

    [HideInInspector]
    public Sprite[] spellIcons;
    public Sprite[] spellIconsGrey;

    private GameObject projectile;

    private void Awake() {
        MakeThisTheOnlySpellManager();
        InitSpellIcons();
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

    private void InitSpells() {
        spells = new Spell[] {
            new Spell("Meteor", 10, projectileMeteor, 40),
            new Spell("Snowball", 30, projectileSnowball, 1)
        };
    }

    public Spell getSpell(string spellName) {
        for (int x = 0; x < spells.Length; x++) {
            if (spells[x].name == spellName) {
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
        if (unitScript.mana < spell.manaCost) {
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

        owner.unit.GetComponent<UnitScript>().mana -= castSpell.manaCost;

        GUIManager.GUI.updateGUI(owner);

        StartCoroutine(SpellDelay(0.2f, owner, castSpell.name, clickPos));
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
