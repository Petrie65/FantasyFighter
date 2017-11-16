using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMeteor : MonoBehaviour {
    public ParticleSystem praticleTail;
    public ParticleSystem particleCollide;

    public Light fireLight;

    private Animator fireLightAnim;
    
    private float speed = 30f;
	private float damage = 75;

    private float radiusAOE = 10f;

    private float distance = 0f;    
	private float range;

    private SphereCollider triggerCollider;

    private Transform startPosition;
    private Player owner;

	private bool isAlive = true;

    private void Awake() {
        triggerCollider = GetComponent<SphereCollider>();
        range = SpellManager.SM.getSpell("Meteor").Info.Range;
        fireLightAnim = fireLight.GetComponent<Animator>();
    }

    private void Update() {
		if (isAlive) {
            if (distance < range) {
                distance++;
			    transform.position += transform.forward * Time.deltaTime * speed;
                this.UpdateHeight();
            } else {
                Explode();
            }
		}
	}

    private void UpdateHeight() {
        RaycastHit hit;

        // Explode if collision with water
        if (transform.position.y < 7.6f) {
            Explode();
            return;
        }

        if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.down) , out hit)) {
                if (hit.transform.tag == ("Ground")){ 
                    float heightPoint = hit.point.y + 2f;
                    this.transform.position = new Vector3(transform.position.x, heightPoint, transform.position.z);
                }
        }
    }

    public void setOwner(Player owner) {
        this.owner = owner;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            UnitScript unitScript = other.gameObject.GetComponent<UnitScript>();
            if (unitScript.owner.name == owner.name) return;
            
            Explode();
        }
    }

    private void Explode() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiusAOE);
        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].gameObject.tag == "Player") {
                GameObject unitObject = hitColliders[i].gameObject;
                UnitScript unitScript = unitObject.GetComponent<UnitScript>();
                if (unitScript.owner != owner) {
                    CapsuleCollider unitCollider = unitObject.GetComponent<CapsuleCollider>();

                    // Calc damange based on distance (only x and y)
                    float proximity = (transform.position - unitObject.transform.position).magnitude;
                    float effect = 1 - (proximity / radiusAOE);
                    float modifiedDamage = damage * effect;

                    if (damage > 0f) {
                        unitScript.TakeDamage(owner, modifiedDamage);

                        // GameManager.GM.objectUIScript.UpdateHealthBar(unitScript.owner.playerNum);
                    }

                    // Apply explosion force
                    unitObject.GetComponent<Rigidbody>().AddExplosionForce(1000f, transform.position, radiusAOE, 1f);
                }
            }
        }

        DestroyProjectile();
    }

    private void DestroyProjectile() {
        isAlive = false;

        GetComponent<MeshRenderer>().enabled = false;
        praticleTail.Stop();

        fireLightAnim.SetTrigger("triggerExplode");

        particleCollide.Emit(30);
        Destroy(gameObject, 1);
    }

}
