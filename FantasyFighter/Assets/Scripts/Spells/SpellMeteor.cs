using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;
using MirzaBeig.ParticleSystems;

public class SpellMeteor : SpellObject {    
    public ParticleSystems ProjectileParticles;
    public GameObject ExplosionParticles;

    private float radiusAOE = 10f;
    private SphereCollider triggerCollider;
    private float distance = 0f;    

	public override void SetPosition() {
        Vector3 pos = Spell.unitScript.transform.position;
        Vector3 modY = new Vector3(pos.x, pos.y + 2f, pos.z);
		this.transform.position = modY;

		this.transform.LookAt(Target);
	}

	public override void StartSpell() {
	}

    private void Awake() {
        triggerCollider = GetComponent<SphereCollider>();
    }

    private void Update() {
		if (Active) {
            if (distance < Spell.Info.Range) {
                distance++;
			    transform.position += transform.forward * Time.deltaTime * Spell.Info.Speed;
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

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            UnitScript unitScript = other.gameObject.GetComponent<UnitScript>();
            if (unitScript.owner.name == Spell.Owner.name) return;
            
            Explode();
        }
    }

    private void Explode() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiusAOE);
        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].gameObject.tag == "Player") {
                GameObject unitObject = hitColliders[i].gameObject;
                UnitScript unitScript = unitObject.GetComponent<UnitScript>();
                if (unitScript.owner.name != Spell.Owner.name) {
                    CapsuleCollider unitCollider = unitObject.GetComponent<CapsuleCollider>();

                    // Calc damange based on distance (only x and y)
                    float proximity = (transform.position - unitObject.transform.position).magnitude;
                    float effect = 1 - (proximity / radiusAOE);
                    float modifiedDamage = Spell.Info.Damage * effect;

                    if (modifiedDamage > 0f) {
                        unitScript.TakeDamage(Spell.Owner, modifiedDamage);
                    }

                    // Apply explosion force
                    unitObject.GetComponent<Rigidbody>().AddExplosionForce(1000f, transform.position, radiusAOE, 1f);
                }
            }
        }

        DestroyProjectile();
    }

    private void DestroyProjectile() {
        Active = false;

        ProjectileParticles.stop();
        ExplosionParticles.SetActive(true);

        GetComponent<MeshRenderer>().enabled = false;

        Destroy(gameObject, 2);
    }

}
