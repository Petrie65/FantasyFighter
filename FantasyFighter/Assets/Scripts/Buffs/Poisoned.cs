using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DuloGames.UI;
using MirzaBeig.ParticleSystems;

public class Poisoned : Buff {
	GameObject particleObject;
	ParticleSystems particles;

	public override void Activate() {
		// 
		TargetUnit.wizardMovement.walkSpeed -= 1f;
		TargetUnit.wizardMovement.runSpeed -= 1f;

		TargetUnit.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = new Color(0.5f, 1f, 0.5f);

		// Set up particles
		particleObject = Instantiate(Info.buffObject, transform);
		particles = particleObject.GetComponent<ParticleSystems>();
	}

	public override void ApplySpell() {
		TargetUnit.TakeDamage(Owner, 0.5f * Intensity);
	}

	public override void End() {
		TargetUnit.wizardMovement.walkSpeed += 1f;
		TargetUnit.wizardMovement.runSpeed += 1f;

		TargetUnit.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f);

		TargetUnit.RemoveBuff(this);

		particles.stop();
		StartCoroutine(WaitForParticles(3f));
	}

	private IEnumerator WaitForParticles(float time) {
		yield return new WaitForSeconds(time);
		Destroy(particleObject);
		Destroy(this);
	}
}