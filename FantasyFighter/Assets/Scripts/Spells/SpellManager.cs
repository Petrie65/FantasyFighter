using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour {
    public static SpellManager SM;
	private string selectedSpell;

    public GameObject meteorProjectile;

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
		if (spell != "")
		{
            GameManager.GM.currentPlayer.unit.GetComponent<UnitScript>().selectedSpell = spell;
			Debug.Log("select spell:" + spell);
		}
	}

	public void CastSpellMouse(int ownerNum, string spellName, Vector3 clickPos) {

        Player owner = GameManager.GM.players[ownerNum];

        switch (spellName) {
            case "Meteor":
                GameObject mProjectile = Instantiate(meteorProjectile);
                mProjectile.transform.position = owner.unit.transform.position;
                mProjectile.transform.LookAt(clickPos);
                mProjectile.GetComponent<MeteorProjectile>().setOwner(owner.name);
                break;

        }
	}
}
