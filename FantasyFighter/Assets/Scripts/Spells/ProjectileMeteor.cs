using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMeteor : MonoBehaviour {
    public ParticleSystem praticleTail;
    public ParticleSystem particleCollide;

    public Light fireLight;

    private Animator fireLightAnim;
    
    private float speed = 30f;
	private float range = 20f;
	private int damage = 10;

    private Transform startPosition;
    private Player owner;

	private bool isAlive = true;

    private void Awake() {
        fireLightAnim = fireLight.GetComponent<Animator>();
    }

    private void Update() {
		if (isAlive) {
			transform.position += transform.forward * Time.deltaTime * speed;
		}
	}

    public void setOwner(Player owner) {
        this.owner = owner;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            UnitScript unitScript = other.gameObject.GetComponent<UnitScript>();
            if (unitScript.owner.name == owner.name) return;

            unitScript.TakeDamage(owner, damage);
            GUIManager.GUI.updateGUI(unitScript.owner);
            GameManager.GM.objectUIScript.UpdateHealthBar(unitScript.owner.playerNum);
            
			isAlive = false;

            GetComponent<MeshRenderer>().enabled = false;
            praticleTail.Stop();

            fireLightAnim.SetTrigger("triggerExplode");

            particleCollide.Emit(30);
            Destroy(gameObject, 1);
        }
    }
    
}
