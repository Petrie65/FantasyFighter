using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour {
    public static SpellManager SM;
	private string selectedSpell;

    public GameObject projectileMeteor;
    public GameObject projectileSnowball;

    private GameObject projectile;

    private void Awake() {
        MakeThisTheOnlySpellManager();
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

    public void SelectSpell(int spellNum)
	{
		var spell = GameManager.GM.currentPlayer.spells[spellNum];
		if (spell != "") {
            GameManager.GM.currentPlayer.unit.GetComponent<UnitScript>().selectedSpell = spell;
            GameManager.GM.currentPlayer.unit.GetComponent<UnitScript>().selectedSpellIdx = spellNum;
			Debug.Log("select spell:" + spell);
		}
	}

	public void CastSpellMouse(int ownerNum, string spellName, Vector3 clickPos) {
        Player owner = GameManager.GM.players[ownerNum];

        owner.unit.GetComponent<UnitScript>().selectedSpell = "";           
        owner.spells[owner.unit.GetComponent<UnitScript>().selectedSpellIdx] = "";
        owner.unit.GetComponent<UnitScript>().selectedSpellIdx = 0;
        GUIManager.GUI.updateGUI(owner.playerNum);

        StartCoroutine(SpellDelay(0.2f, owner, spellName, clickPos));
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
        mProjectile.SendMessage("setOwner", owner.name);
    }
}
