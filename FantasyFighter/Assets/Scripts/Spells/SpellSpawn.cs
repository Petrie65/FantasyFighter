using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawn : MonoBehaviour {
	public GameObject worldSpell;
	GameObject spellObject;

	public ParticleSystem particles;

	bool creatingNewSpell = false;

	private void Start() {
		CreateSpell();
	}
	
    private void Update() {
		if (!creatingNewSpell) {
			if (spellObject == null) {
				StartCoroutine(CreateNewSpell());
			} 
		}
	}

	private IEnumerator CreateNewSpell() {
		creatingNewSpell = true;

		yield return new WaitForSeconds(5f);

		particles.Emit(3000);

		yield return new WaitForSeconds(0.5f);

		CreateSpell();
		creatingNewSpell = false;
	}	

	private void CreateSpell() {
		spellObject = SpellManager.SM.AddWorldSpell(worldSpell);
		spellObject.transform.position = this.transform.position;
	}
}
