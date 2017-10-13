using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour {
	private string selectedSpell;

	public void SelectSpell(int spellNum)
	{
		var spell = GameManager.GM.currentPlayer.spells[spellNum];
		if (spell != "")
		{
			Debug.Log("select spell:" + spell);
		}
	}

	public void CastSpell(int ownerNum, string spellName)
	{

	}
}
