using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DuloGames.UI;
using MirzaBeig.ParticleSystems;

public class Poisoned : Buff {

	public override void Activate() {
		TargetUnit.wizardMovement.walkSpeed -= 1f;
		TargetUnit.wizardMovement.runSpeed -= 1f;
	}

	public override void ApplySpell() {
		TargetUnit.TakeDamage(Owner, 0.5f * Intensity);
	}

	public override void End() {
		TargetUnit.wizardMovement.walkSpeed += 1f;
		TargetUnit.wizardMovement.runSpeed += 1f;

		particles.stop();
	}
}