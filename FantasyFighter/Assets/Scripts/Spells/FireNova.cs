using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireNova : SpellObject {
	public ParticleSystem[] particles;
	public Light effectLight;

	private void Start() {
		StartCoroutine(WaitForDisable(0.5f));
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            UnitScript unitScript = other.gameObject.GetComponent<UnitScript>();
            if (unitScript.owner.name == Spell.Owner.name) return;
			
        	unitScript.AddBuff<Burning>(Spell.Owner, 3f, 1, 1f);
		}
	}

	private IEnumerator WaitForDisable(float time) {
		yield return new WaitForSeconds(time);
		GetComponent<SphereCollider>().enabled = false;
	}

	public void DestroyAfterLight() {
		Destroy(gameObject);
	}
}
