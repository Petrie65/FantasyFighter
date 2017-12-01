using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DuloGames.UI;

public class Poisoned : Buff {
	GameObject particleObject;
	ParticleSystem[] particles;

	public override void Activate() {
		// Set up particles
		particleObject = Instantiate(Info.buffObject, transform);
		int particleNum = particleObject.transform.childCount;
		particles = new ParticleSystem[particleNum];
		for (int x = 0; x < particleNum; x++) {
			GameObject child = particleObject.transform.GetChild(x).gameObject;
			particles[x] = child.GetComponent<ParticleSystem>();
			// particles[x]
		}
	}

	public override void ApplySpell() {
		TargetUnit.TakeDamage(Owner, 0.5f * Intensity);
	}

	public override void End() {
		TargetUnit.RemoveBuff(this);

		foreach(ParticleSystem particle in particles) {
			particle.Stop();
		}
		StartCoroutine(WaitForParticles(3f));
	}

	private IEnumerator WaitForParticles(float time) {
		yield return new WaitForSeconds(time);
		Destroy(particleObject);
		Destroy(this);
	}
}