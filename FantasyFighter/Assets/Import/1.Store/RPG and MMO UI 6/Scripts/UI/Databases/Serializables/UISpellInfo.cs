using UnityEngine;
using System;

namespace DuloGames.UI
{
	public enum CastTypes { Channel, Charge, Instant };

	[Serializable]
	public class UISpellInfo
	{
		public int ID;
		public string Name;
		public Sprite Icon;

		public string Description;

		public float Range;
		public float Cooldown;
		public float CastTime;
		public float PowerCost;
			
    	// Aditional vars
		public float Damage;
		public float HoldTime;
		public CastTypes CastType;
		public int Amount;

		public GameObject worldObject;
		public GameObject spellObject;
	
		[BitMask(typeof(UISpellInfo_Flags))]
		public UISpellInfo_Flags Flags;
	}
}
