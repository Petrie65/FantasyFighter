using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using DuloGames.UI;
using MirzaBeig.ParticleSystems;

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

	// Particles
	public virtual GameObject particleObject {get; protected set;}
	public virtual ParticleSystems particles {get; protected set;}
	public virtual IEnumerator coroutine {get; protected set;}

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
			SetupParticles();
			Activate();
		} else {
            ConsoleProDebug.LogToFilter("Buff does not match up with name in DB", "Spell");
		}
	}

	public void SetupParticles() {
		particleObject = Instantiate(Info.buffObject, transform);
		particles = particleObject.GetComponent<ParticleSystems>();
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
		if (IsFinished) {
			particles.play();
			StopCoroutine(coroutine);
			
			IsFinished = false;
			Activate();
		}
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

	
	public IEnumerator WaitForParticles(float time) {
		particles.stop();

		yield return new WaitForSeconds(time);

		ConsoleProDebug.LogToFilter("Particles done", "Spell");

		TargetUnit.RemoveBuff(this);
		Destroy(particleObject);
		Destroy(this);
	}

}


	
