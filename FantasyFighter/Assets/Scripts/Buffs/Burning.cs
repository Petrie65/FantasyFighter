using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DuloGames.UI;

public class Burning : Buff {

	public override void Activate() {
		// movementComponent.moveSpeed += speedBuff.SpeedIncrease;
		// GuiManager.GUI.AddBuff();

		Debug.Log("Activate");
	}

	public override void ApplySpell() {

		Debug.Log("ApplySpell");
	}

	public override void End() {
		// movementComponent.moveSpeed -= speedBuff.SpeedIncrease;

		Debug.Log("End");
		// GuiManager.GUI.RemoveBuff();
		unitScript.RemoveBuff(this);
	}
}