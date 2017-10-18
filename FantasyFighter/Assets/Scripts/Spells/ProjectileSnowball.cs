using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSnowball : MonoBehaviour {
    // public ParticleSystem praticleTail;
    // public ParticleSystem particleCollide;

    public Light fireLight;

    // private Animator fireLightAnim;
    
    private float speed = 20f;
	private float range = 20f;
	private int damage = 10;

    private Transform startPosition;
    private string owner;

	private bool isAlive = true;

    private void Awake() {
        // fireLightAnim = fireLight.GetComponent<Animator>();
    }

    private void Update() {
		if (isAlive) { 
            // transform.Rotate(0, 10f, 0, Space.Self);
			transform.position += transform.forward * Time.deltaTime * speed;   

		}
	}

    public void setOwner(string ownerName) {
        owner = ownerName;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("Collision owner name: " + other.gameObject.GetComponent<UnitScript>().ownerName);
            if (other.gameObject.GetComponent<UnitScript>().ownerName == owner) return;

			isAlive = false;

            GetComponent<MeshRenderer>().enabled = false;
            // praticleTail.Stop();

            // fireLightAnim.SetTrigger("triggerExplode");

            // particleCollide.Emit(30);
            Destroy(gameObject, 1);
        }
    }
    
}
