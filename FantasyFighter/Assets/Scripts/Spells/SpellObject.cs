using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

public abstract class SpellObject : MonoBehaviour {
	public virtual bool Active {get; protected set;}	
	public virtual Spell Spell {get; protected set;}	
	public virtual Vector3 Target {get; protected set;}	

	public void Init(Spell spell, Vector3 target) {
		Active = true;
		Spell = spell;
		Target = target;

        ConsoleProDebug.LogToFilter("(" + Spell.Owner.name + ") Launched: " + spell.Info.Name, "Spell");

		// Set position before activating spell
		SetPosition();
		// Activate Spell
		this.gameObject.SetActive(true);
		// Start spell
		StartSpell();
	}

	public abstract void SetPosition();
	public abstract void StartSpell();

	public Vector3 GetCastPosition(Vector3 target) {
		return Vector3.zero;
	}

    public float CalcCharge(float statValue, float headroom,string debugString) {
        float range = statValue * headroom;
        float charge = Spell.ChargePercentage * range;
        ConsoleProDebug.LogToFilter(debugString + " : " + Mathf.Round(statValue + charge) + "(" + statValue + "-" + statValue * (headroom+1) + ")", "Spell");
        return statValue + charge;
    }

	void OnDestroy() {
        ConsoleProDebug.LogToFilter("(" + Spell.Owner.name + ") Destroyed: " + this.GetType().Name, "Spell");
	}
}
