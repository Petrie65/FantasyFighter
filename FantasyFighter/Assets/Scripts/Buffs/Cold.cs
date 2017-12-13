using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DuloGames.UI;
using MirzaBeig.ParticleSystems;

public class Cold : Buff {

	public override void Activate() {
		TargetUnit.wizardMovement.walkSpeed -= 2f;
		TargetUnit.wizardMovement.runSpeed -= 2f;
	}

	public override void ApplySpell() {
	}

	public override void End() {
		TargetUnit.wizardMovement.walkSpeed += 2f;
		TargetUnit.wizardMovement.runSpeed += 2f;

		particles.stop();
	}
}