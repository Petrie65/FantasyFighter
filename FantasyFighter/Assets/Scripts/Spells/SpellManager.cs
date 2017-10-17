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
            GameManager.GM.currentPlayer.unit.GetComponent<UnitScript>().selectedSpellIdx = spellNum;
			Debug.Log("select spell:" + spell);
		}
	}

	public void CastSpellMouse(int ownerNum, string spellName, Vector3 clickPos) {

        Player owner = GameManager.GM.players[ownerNum];

        owner.unit.GetComponent<UnitScript>().selectedSpell = "";           
        owner.spells[owner.unit.GetComponent<UnitScript>().selectedSpellIdx] = "";
        owner.unit.GetComponent<UnitScript>().selectedSpellIdx = 0;
        GUIManager.GUI.updateGUI(0);

        switch (spellName) {
            case "Meteor":
				StartCoroutine(SpellDelay(0.3f, clickPos, owner));
                break;

        }
	}
	
	private IEnumerator SpellDelay(float delay, Vector3 clickPos, Player owner) {

		yield return new WaitForSeconds(delay);

		GameObject mProjectile = Instantiate(meteorProjectile);
		mProjectile.transform.position = owner.unit.transform.position;
		mProjectile.transform.LookAt(clickPos);
		mProjectile.GetComponent<MeteorProjectile>().setOwner(owner.name);
	}
}
