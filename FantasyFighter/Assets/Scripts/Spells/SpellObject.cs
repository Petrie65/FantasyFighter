using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

public class SpellObject : MonoBehaviour {
	public virtual bool Active {get; protected set;}	
	public virtual Spell Spell {get; protected set;}	

	public void Init(Spell spell, Vector3 target) {
		Active = true;

        ConsoleProDebug.LogToFilter("(" + Spell.Owner.name + ")Launched: " + spell.Info.Name, "Spell");

		gameObject.SetActive(true);
	}

	void Start () {

	}
	
	void Update () {
		
	}

	void OnDestroy() {
        ConsoleProDebug.LogToFilter("(" + Spell.Owner.name + ")Destroyed: " + this.GetType().Name, "Spell");
	}
}
