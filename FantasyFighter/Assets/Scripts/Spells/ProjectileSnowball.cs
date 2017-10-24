using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSnowball : MonoBehaviour {
    public ParticleSystem praticleTail;
    public ParticleSystem particleCollide;

    public Light iceLight;

    private Animator iceLightAnim;
    
    private float speed = 20f;
	private float range = 20f;
	private int damage = 10;

    private Transform startPosition;
    private string owner;

	private bool isAlive = true;

    private void Awake() {
        iceLightAnim = iceLight.GetComponent<Animator>();
    }

    private void Update() {
		if (isAlive) { 
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
            praticleTail.Stop();

            iceLightAnim.SetTrigger("triggerExplode");

            particleCollide.Emit(300);
            Destroy(gameObject, 1);
        }
    }
    
}
