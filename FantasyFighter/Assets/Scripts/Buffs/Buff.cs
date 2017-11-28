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
	public virtual UnitScript unitScript {get; protected set;}
	public virtual GameObject GUIItem {get; protected set;}

	// Stack
	public virtual float Duration {get; protected set;}
	public virtual int StackSize {get; protected set;}

	public virtual float CurrentTime {get; protected set;}
	public virtual bool IsFinished {get; protected set;}

	public Buff() {
		IsFinished = false;
		CurrentTime = 0f;
		StackSize = 0;
	}

	private void Start() {
		
		Duration = 20f;

		string buffName = this.GetType().Name;
		Info =  UIBuffDatabase.Instance.GetByName(buffName);
		Activate();
	}

	public void AddStack() {
		if (StackSize < Info.MaxStack) StackSize++;

		// Reset current time each time a stack is added
		ResetTime();
	}

	public void ResetTime() {
		CurrentTime = 0f;
	}

	private void Update() {
		if (!IsFinished) {
			CurrentTime += Time.deltaTime;

			// Update buff to show time left
			// GuiManager.GUI.UpdateBuff();

			ApplySpell();
				
			if(CurrentTime >= Duration) {
				IsFinished = true;
				End();
			}
		}
	}

	public abstract void Activate();
	// If the spell is active, this will get called
	public abstract void ApplySpell();
	public abstract void End();
}


	
