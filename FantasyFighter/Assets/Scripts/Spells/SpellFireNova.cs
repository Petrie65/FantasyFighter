using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFireNova : MonoBehaviour {
	public ParticleSystem[] particles;
	public Light light;

    private Player owner;

	private void Start() {
		StartCoroutine(WaitForDisable(0.5f));
	}

	private void Update() {
		if (light.intensity > 0) {
			light.intensity -= 0.4f;
		}
	}

    public void setOwner(Player owner) {
        this.owner = owner;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            UnitScript unitScript = other.gameObject.GetComponent<UnitScript>();
            if (unitScript.owner.name == owner.name) return;
			
        	unitScript.AddBuff<Burning>(owner, 3f, 1, 1f);
		}
	}

	private IEnumerator WaitForDisable(float time) {
		yield return new WaitForSeconds(time);
		GetComponent<SphereCollider>().enabled = false;

		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
