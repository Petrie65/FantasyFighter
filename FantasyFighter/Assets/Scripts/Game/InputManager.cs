using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	
	void Update () {
		HandleKeyboardInput();	
	}

	public void HandleKeyboardInput() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) { SelectSpell(0); }
		if (Input.GetKeyDown(KeyCode.Alpha2)) { SelectSpell(1); }
		if (Input.GetKeyDown(KeyCode.Alpha3)) { SelectSpell(2); }
		if (Input.GetKeyDown(KeyCode.Alpha4)) { SelectSpell(3); }
		if (Input.GetKeyDown(KeyCode.Alpha5)) { SelectSpell(4); }

		if (Input.GetKeyDown(KeyCode.Escape)) { ClearSpells(); }
	}  

	public void ClickSpellButton(int idx) {
		SelectSpell(idx);
	}

	private void SelectSpell(int spellIdx) {
		SpellManager.SM.SelectSpell(spellIdx);
	}

	private void ClearSpells() {
		Debug.Log("Esc pressed");
		GameManager.GM.currentPlayer.unit.GetComponent<UnitScript>().UnselectSpells();
	}
}
