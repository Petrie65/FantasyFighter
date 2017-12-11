using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MirzaBeig.ParticleSystems;
using DG.Tweening;

public class GlacialBlast : SpellObject {
    public GameObject Projectile;
    public GameObject FrostObject;

    public Light effectLight;

    private ParticleSystems projectileParticles;
    private GlacialBlastFrost frostScript;

    private float radiusAOE;
    private SphereCollider triggerCollider;

    private float distance = 0f;    

    private float speed = 0f;
    private float range = 0f;

    public override void SetPosition() {
        Vector3 pos = Spell.unitScript.transform.position;
        Vector3 modY = new Vector3(pos.x, pos.y + 2f, pos.z);
		this.transform.position = modY;

		this.transform.LookAt(Target);
	}

    private void Awake() {
        triggerCollider = GetComponent<SphereCollider>();
        projectileParticles = Projectile.GetComponent<ParticleSystems>();
        frostScript = FrostObject.GetComponent<GlacialBlastFrost>();

        radiusAOE = FrostObject.GetComponent<SphereCollider>().radius;
    }

	public override void StartSpell() {
        speed = CalcCharge(Spell.Info.Speed, 0.5f, "Speed");
        range = CalcCharge(Spell.Info.Range, 1f, "Range");

        effectLight.DOIntensity(2f, 0.5f);
	}

    private void Update() {
		if (Active) {
            if (distance < range) {
                float moveDistance =  Time.deltaTime * speed;
                distance += moveDistance;
			    transform.position += transform.forward.normalized * moveDistance;
                this.UpdateHeight();
            } else {
                DestroyProjectile(false);
            }
		}
	}

    private void UpdateHeight() {
        RaycastHit hit;

        // Explode if collision with water
        if (transform.position.y < 7.6f) {
            DestroyProjectile(false);
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
                    unitScript.AddBuff<Cold>(Spell.Owner, 3f, 1, 1f);
                    unitScript.TakeDamage(Spell.Owner, Spell.Info.Damage);
                }
            }
        }
        
        FrostObject.SetActive(true);
        frostScript.StartSpell(Spell.Owner, 4f);

        DestroyProjectile(true);
    }

    private void DestroyProjectile(bool isExplosion) {
        Active = false;
        triggerCollider.enabled = false;
        
        projectileParticles.setLoop(false);

        if (isExplosion) {
            Sequence mySequence = DOTween.Sequence();
            mySequence
                .Append(effectLight.DOIntensity(6f, 0.5f))
                .Append(effectLight.DOIntensity(0f, 0.5f));

            Destroy(gameObject, 7f);
        } else {
            effectLight.DOIntensity(0f, 1f);
            Destroy(gameObject, 3f);
        }
    }    

}
