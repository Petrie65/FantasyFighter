using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DuloGames.UI;

public class Burning : Buff {

	public override void Activate() {
	}

	public override void ApplySpell() {
		TargetUnit.TakeDamage(Owner, 0.5f * Intensity);
	}

	public override void End() {
		particles.stop();
	}
}