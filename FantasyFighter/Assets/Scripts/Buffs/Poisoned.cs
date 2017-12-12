using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DuloGames.UI;
using MirzaBeig.ParticleSystems;

public class Poisoned : Buff {

	public override void Activate() {
		coroutine = WaitForParticles(3f);

		TargetUnit.wizardMovement.walkSpeed -= 1f;
		TargetUnit.wizardMovement.runSpeed -= 1f;

		TargetUnit.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = new Color(0.65f, 1f, 0.65f);
	}

	public override void ApplySpell() {
		TargetUnit.TakeDamage(Owner, 0.5f * Intensity);
	}

	public override void End() {
		TargetUnit.wizardMovement.walkSpeed += 1f;
		TargetUnit.wizardMovement.runSpeed += 1f;

		TargetUnit.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f);

		StartCoroutine(coroutine);
		ConsoleProDebug.LogToFilter("End - wait for particles", "Spell");
	}
}