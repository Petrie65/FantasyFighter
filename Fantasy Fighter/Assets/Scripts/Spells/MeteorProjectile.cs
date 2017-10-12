using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorProjectile : MonoBehaviour {
    public ParticleSystem praticleTail;
    public ParticleSystem particleCollide;

    public Light fireLight;

    private Animator fireLightAnim;
    
    private float speed = 50f;
    private int damage = 10;
    private float range = 20f;

    private Transform startPosition;
    private string owner;

    private void Awake() {
        fireLightAnim = fireLight.GetComponent<Animator>();
    }

    private void Update() {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            GetComponent<MeshRenderer>().enabled = false;
            praticleTail.Stop();

            fireLightAnim. Play("triggerExplode");

            particleCollide.Emit(30);
            Destroy(gameObject, 1);
        }
    }
    
}
