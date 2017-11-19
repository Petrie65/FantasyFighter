using UnityEngine;
using System;

namespace DuloGames.UI
{
	public enum MoveType { None, Walk, Run };

	[Serializable]
	public class UISpellInfo
	{
		[Header("Spell")]
		public int ID;
		public string Name;
		public Sprite Icon;
		public string Description;

		public Color ChannelColor;
		public Color ChargeColor;

		[Header("Execution")]
		public int Amount;
		public float Range;
		public float Damage;
		public float Cooldown;

		[Header("Casting")]
		public float ChannelTime; // 0 is instant
		public float PowerCost;

		public float ChargeTo; // 0 is no charge

		public float HoldTo; // 0 is infinie

		public bool ShouldFail;
		public float DecayRate;

		public MoveType ChannelMove;

		[Header("Objects")]
		public GameObject worldObject;
		public GameObject spellObject;
	
		[BitMask(typeof(UISpellInfo_Flags))]
		public UISpellInfo_Flags Flags;
	}
}
