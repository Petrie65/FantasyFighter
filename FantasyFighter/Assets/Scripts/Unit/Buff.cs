using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public abstract class Buff : MonoBehaviour {
	[EnumToggleButtons]
	enum BuffType{
		Burning,
		Poisoned,
		Slowed
	}

	// Details
	string Name;
	bool IsPositive;
	BuffType Type;

	// Image Icon;

	Player owner;
	UnitScript unitScript;

	// Buff removal (duration or trigger)
	public bool IsFinished = false;
	bool RemoveTime;
	float Duration;

	// Stack
	bool Stackable;
	int MaxStack;

	private float CurrentTime = 0f;
	private int StackSize = 0;

	public void AddStack() {
		StackSize++;
		// Reset current time each time a stack is added
		CurrentTime = 0f;
	}

	public void ResetTime() {
		CurrentTime = 0f;
	}

	public abstract void Activate();
	public abstract void Update();
	public abstract void End();
}

public class Slow : Buff {
	public override void Activate() {
		// StackSize = 1;
		// movementComponent.moveSpeed += speedBuff.SpeedIncrease;
		// GuiManager.GUI.AddBuff();
	}

	public override void Update() {
		// CurrentTime += delta;

		// if (RemoveTime) {
		// 	// Update buff to show time left
		// 	GuiManager.GUI.UpdateBuff();
		// }

		// if(CurrentTime >= Duration)
		// 	End();
	}

	public override void End() {
		// StackSize = 0;
		// movementComponent.moveSpeed -= speedBuff.SpeedIncrease;

		// GuiManager.GUI.RemoveBuff();
	}

}
	
	 // Unit
/*public List<Buff> CurrentBuffs = new List<Buff>();


void Update() {
foreach(Buff buff in CurrentBuffs.ToArray()) {
buff.Update(Time.deltaTime);
if (buff.IsFinished) {
CurrentBuffs.Remove(buff);
}
}
}

public void AddBuff(Buff buff) {
bool exists = CurrentBuffs.Contains(buff);

var currentBuff = GetBuff(buff);
if (currentBuff == null) {
// Unit does not have buff yet
CurrentBuffs.Add(buff);
buff.Activate();
} else {
// Buff already exists
if (currentBuff.Stack) {
currentBuff.AddStack();
} else {
currentBuff.ResetTime();
}
}
}

private Buff GetCurrentBuff(Buff buff) {
foreach(Buff has in CurrentBuffs.ToArray()) {
if (has.Type === buff.Type) {
return has;
}
}
Debug.Log("Could not retrieve specified buff");
return null;
}

public Buff GetBuff(Buff buff) {
foreach(Buff has in CurrentBuffs.ToArray()) {
if (has.Type === buff.Type) {
return has;
}
}
Debug.Log("Buff does not exist");
return null;
}
}*/

