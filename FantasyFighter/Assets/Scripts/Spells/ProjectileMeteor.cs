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

    private float distance = 0f;    
	private float range;

    private SphereCollider triggerCollider;

    private Transform startPosition;
    private Player owner;

	private bool isAlive = true;

    private void Awake() {
        triggerCollider = GetComponent<SphereCollider>();
        range = SpellManager.SM.getSpell("Meteor").Range;
        fireLightAnim = fireLight.GetComponent<Animator>();
    }

    private void Update() {
		if (isAlive) {
            if (distance < range) {
                distance++;
			    transform.position += transform.forward * Time.deltaTime * speed;
            } else {
                Explode();
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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);

        for (int i = 0; i < hitColliders.Length; i++) {
            Debug.Log("Collide: " + hitColliders[i].gameObject.name);
            
            if (hitColliders[i].gameObject.tag == "Player") {
                UnitScript unitScript = hitColliders[i].gameObject.GetComponent<UnitScript>();

                // Calc damange based on distance
                    //TODO, deduct collider radius
                float distance = Vector3.Distance(transform.position, hitColliders[i].transform.position) + triggerCollider.radius;

                if (distance <= 1f) {
                    Debug.Log("distance: " + distance.ToString());
                }
                float modifiedDamage = damage / distance;

                unitScript.TakeDamage(owner, modifiedDamage);
                Debug.Log("Damage: " + modifiedDamage.ToString());

                GUIManager.GUI.updateGUI(unitScript.owner);
                GameManager.GM.objectUIScript.UpdateHealthBar(unitScript.owner.playerNum);
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
