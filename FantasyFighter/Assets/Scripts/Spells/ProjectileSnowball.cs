using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSnowball : MonoBehaviour {
    public ParticleSystem praticleTail;
    public ParticleSystem[] particlesCollision;
    public int[] emitCount;

    public Light iceLight;

    private Animator iceLightAnim;
    
    private float speed = 15f;
	private float range = 20f;
	private int damage = 10;

    private Transform startPosition;
    private Player owner;

	private bool isAlive = true;

    private void Awake() {
        iceLightAnim = iceLight.GetComponent<Animator>();
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

            iceLightAnim.SetTrigger("triggerExplode");

            for (int x=0;x<particlesCollision.Length;x++) {
                if (particlesCollision[x]) {
                    particlesCollision[x].Emit(emitCount[x]);
                }
            }

            Destroy(gameObject, 1);
        }
    }
    
}
