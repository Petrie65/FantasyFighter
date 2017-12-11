using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DuloGames.UI;
using MirzaBeig.ParticleSystems;

public class Cold : Buff {
	GameObject particleObject;
	ParticleSystems particles;

	public override void Activate() {
		// 
		TargetUnit.wizardMovement.walkSpeed -= 2f;
		TargetUnit.wizardMovement.runSpeed -= 2f;

		TargetUnit.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = new Color(0.65f, 1f, 0.65f);

		// Set up particles
		particleObject = Instantiate(Info.buffObject, transform);
		particles = particleObject.GetComponent<ParticleSystems>();
	}

	public override void ApplySpell() {
	}

	public override void End() {
		TargetUnit.wizardMovement.walkSpeed += 2f;
		TargetUnit.wizardMovement.runSpeed += 2f;

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