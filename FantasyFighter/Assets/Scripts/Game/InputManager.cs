using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	
	void Update () {
		HandleKeyboardInput();	
	}

	public void HandleKeyboardInput() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			SpellManager.SM.SelectSpell(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			SpellManager.SM.SelectSpell(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			SpellManager.SM.SelectSpell(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			SpellManager.SM.SelectSpell(3);
		}
	}
}
