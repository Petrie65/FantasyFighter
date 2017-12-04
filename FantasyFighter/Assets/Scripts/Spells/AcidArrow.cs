using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;
using DG.Tweening;
using MirzaBeig.ParticleSystems;

public class AcidArrow : SpellObject {
    public ParticleSystems arrowParticles;
    public ParticleSystems collisionParticles;

    public Light arrowLight;

    private float distance = 0f;    

    private float speed = 0f;
    private float range = 0f;
    private float damage = 0f;

	public override void SetPosition() {
        Vector3 pos = Spell.unitScript.transform.position;
        Vector3 modY = new Vector3(pos.x, pos.y + 2f, pos.z);
		this.transform.position = modY;

		this.transform.LookAt(Target, Vector3.up);
	}

	public override void StartSpell() {
        speed = CalcCharge(Spell.Info.Speed, 1f, "Speed");
        range = CalcCharge(Spell.Info.Range, 3f, "Range");
        damage = CalcCharge(Spell.Info.Damage, 1f, "Damage");;

        arrowLight.DOIntensity(8f, 0.2f);
        this.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.5f);
	}

    private void Update() {
		if (Active) {
            if (distance < range) {
                float moveDistance =  Time.deltaTime * speed;
                distance += moveDistance;
                
			    transform.position += transform.forward.normalized * moveDistance;
                Debug.Log("Move");
                this.UpdateHeight();
            } else {
                Debug.Log("distance break");
                Break();
            }
		}
	}

    private void UpdateHeight() {
        RaycastHit hit;

        // Explode if collision with water
        if (transform.position.y < 7.6f) {
            Debug.Log("height break");
            Break();
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
            
            unitScript.TakeDamage(Spell.Owner, damage);
        	unitScript.AddBuff<Poisoned>(Spell.Owner, 5f, 1, 0.1f);

            collisionParticles.transform.position = this.transform.position;
            Instantiate(collisionParticles);

            Debug.Log("collision break");
            Break();
        }
    }

    private void Break() {
        Active = false;

        this.GetComponent<BoxCollider>().enabled = false;

        arrowParticles.setLoop(false);

        // Finish possible current tweens before tweening down
        this.transform.DOComplete();

        arrowLight.DOIntensity(0f, 0.25f);
        this.transform.DOScale(Vector3.zero, 0.25f);

        Destroy(gameObject, 1);
    }
}
