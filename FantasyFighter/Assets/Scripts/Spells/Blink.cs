using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MirzaBeig.ParticleSystems;

public class Blink : SpellObject {
	// public Light fromLight;
	// public Light toLight;

	public ParticleSystems fromParticles;
	public ParticleSystems toParticles;

	public override void SetPosition() {
		this.transform.position = Spell.unitScript.transform.position;
	}

	public override void StartSpell() {
		fromParticles.play();

		toParticles.transform.position = Target;
		toParticles.play();

		StartCoroutine(WaitForTP(0.5f));
	}

	private IEnumerator WaitForTP(float time) {
		yield return new WaitForSeconds(time);
		
		Spell.unitScript.wizardMovement.SetPosition(Target);
		Destroy(this, 1f);
	}


}
