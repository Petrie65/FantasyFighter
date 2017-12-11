using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using DuloGames.UI;

public abstract class Buff : MonoBehaviour {
	// Info
	public virtual UIBuffInfo Info {get; protected set;}

	// Player + Unit
	public virtual Player Owner {get; protected set;}
	public virtual UnitScript TargetUnit {get; protected set;}
	public virtual GameObject GUIItem {get; protected set;}

	// Stack
	public virtual float Duration {get; protected set;}
	public virtual int StackSize {get; protected set;}
	public virtual float Intensity {get; protected set;}

	public virtual float CurrentTime {get; protected set;}
	public virtual bool IsFinished {get; protected set;}

	public Buff() {
		IsFinished = false;
		CurrentTime = 0f;
		StackSize = 0;
	}

	public void Init(UnitScript unit, Player owner, float duration, int stacks, float intensity) {
		TargetUnit = unit;	
		Owner = unit.owner;
		Duration = duration;
		StackSize = stacks;
		Intensity = intensity;

		string buffName = this.GetType().Name;
		Info =  UIBuffDatabase.Instance.GetByName(buffName);
		
		if (Info != null) {
			ConsoleProDebug.LogToFilter("Activate", "Spell");
			Activate();
		} else {
            ConsoleProDebug.LogToFilter("Buff does not match up with name in DB", "Spell");
		}
	}

	public void AddStack() {
		ConsoleProDebug.LogToFilter("Add stack", "Spell");

		if (StackSize < Info.MaxStack) StackSize++;

		// Reset current time each time a stack is added
		ResetTime();
	}

	public void ResetTime() {
		ConsoleProDebug.LogToFilter("Reset time", "Spell");

		CurrentTime = 0f;
		IsFinished = false;

		// StopCoroutine( "DoAction" );
	}

	private void Update() {
		if (!IsFinished) {	
			if(CurrentTime >= Duration) {
				IsFinished = true;
				ConsoleProDebug.LogToFilter("End", "Spell");
				End();
			} else {
				CurrentTime += Time.deltaTime;
				ApplySpell();
			}			
		}
	}

	// Buff start
	public abstract void Activate();

	// If the buff is active, this will get called
	public abstract void ApplySpell();

	// Buff fin
	public abstract void End();
}


	
