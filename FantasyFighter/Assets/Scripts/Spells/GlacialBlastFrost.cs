using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GlacialBlastFrost : MonoBehaviour {
    private Player owner;

	public void StartSpell(Player owner, float duration) {
        this.owner = owner;
		StartCoroutine(WaitForDisable(duration));
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            UnitScript unitScript = other.gameObject.GetComponent<UnitScript>();
            if (unitScript.owner.name == owner.name) return;
			
        	unitScript.AddBuff<Cold>(owner, 3f, 1, 1f);
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
