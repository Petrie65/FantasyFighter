using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorProjectile : MonoBehaviour {
    public ParticleSystem praticleTail;
    public ParticleSystem particleCollide;

    public Light fireLight;

    private Animator fireLightAnim;
    
    private float speed = 30f;
	private float range = 20f;
	private int damage = 10;

    private Transform startPosition;
    private string owner;

	private bool isAlive = true;

    private void Awake() {
        fireLightAnim = fireLight.GetComponent<Animator>();
    }

    private void Update() {
		if (isAlive) {
			transform.position += transform.forward * Time.deltaTime * speed;
		}
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
			isAlive = false;

            GetComponent<MeshRenderer>().enabled = false;
            praticleTail.Stop();

            fireLightAnim.SetTrigger("triggerExplode");

            particleCollide.Emit(30);
            Destroy(gameObject, 1);
        }
    }
    
}
