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

	public override void SetPosition() {
        Vector3 pos = Spell.unitScript.transform.position;
        Vector3 modY = new Vector3(pos.x, pos.y + 2f, pos.z);
		this.transform.position = modY;

		this.transform.LookAt(Target, Vector3.up);
	}

	public override void StartSpell() {
        float chargePercentage = Spell.chargeCounter / Spell.Info.ChargeTo;

        float baseSpeed = Spell.Info.Speed;
        float headroom = baseSpeed * 0.5f;
        float chargeSpeed = chargePercentage * headroom;
        speed = baseSpeed + chargeSpeed;

        float baseRange = Spell.Info.Range;
        float headroomR = baseRange * 2f;
        float chargeRange = chargePercentage * headroomR;
        range = baseRange + chargeRange;
	}

    private void Update() {
		if (Active) {
            if (distance < range) {
                distance++;
			    transform.position += transform.forward.normalized * Time.deltaTime * speed;
                this.UpdateHeight();
            } else {
                Break();
            }
		}
	}

    private void UpdateHeight() {
        RaycastHit hit;

        // Explode if collision with water
        if (transform.position.y < 7.6f) {
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
            
            Break();
        }
    }

    private void Break() {
        Active = false;

        arrowParticles.stop();

        collisionParticles.transform.position = this.transform.position;
        Instantiate(collisionParticles);

        // GetComponent<MeshRenderer>().enabled = false;

        this.transform.DOScale(Vector3.zero, 0.5f);
        Destroy(gameObject, 1);
    }
}
