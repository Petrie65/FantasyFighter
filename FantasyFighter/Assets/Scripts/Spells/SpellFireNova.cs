using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFireNova : MonoBehaviour {
	public ParticleSystem[] particles;
	public Light light;

    private Player owner;

	private void Start() {
		
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
			
			Debug.Log("player hit");
		}
	}
}
