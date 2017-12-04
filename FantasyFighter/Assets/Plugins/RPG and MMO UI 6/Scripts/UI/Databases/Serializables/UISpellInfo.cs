using UnityEngine;
// using UnityEditor;
using System;
using Sirenix.OdinInspector;

namespace DuloGames.UI
{
	public enum MoveType { None, Walk, Run };

	[Serializable]
	public class UISpellInfo
	{
		[FoldoutGroup("$Name")] public int ID;
		[FoldoutGroup("$Name")] public string Name;
		[FoldoutGroup("$Name")] public Sprite Icon;
		[FoldoutGroup("$Name")] public string Description;

		[FoldoutGroup("$Name/Colors")] public Color ChannelColor;
		[FoldoutGroup("$Name/Colors")] public Color ChargeColor;

		[FoldoutGroup("$Name/Execution")] [Range(0, 10)] public int Amount;
		[FoldoutGroup("$Name/Execution")] [Range(0, 100)] public float Range;
		[FoldoutGroup("$Name/Execution")] [Range(0, 100)] public float Damage;
		[FoldoutGroup("$Name/Execution")] [Range(0, 100)] public float Speed;
		[FoldoutGroup("$Name/Execution")] [Range(0, 100)] public float Cooldown;
		[FoldoutGroup("$Name/Execution")] public MoveType ChannelMove;

		[FoldoutGroup("$Name/Cast")] [Range(0, 10)] public float ChannelTime; // 0 is instant
		[FoldoutGroup("$Name/Cast")] [Range(0, 10)] public float PowerCost;
		[FoldoutGroup("$Name/Cast")] [Range(0, 10)] public float ChargeTo; // 0 is no charge
		[FoldoutGroup("$Name/Cast")] [Range(0, 10)] public float HoldTo; // 0 is infinie
		[FoldoutGroup("$Name/Cast")] [Range(0, 10)] public float DecayRate;
		[FoldoutGroup("$Name/Cast")] public bool ShouldFail;

		[InlineEditor(InlineEditorModes.SmallPreview)]
		[FoldoutGroup("$Name/Objects")]public GameObject worldObject;
		[InlineEditor(InlineEditorModes.SmallPreview)]
		[FoldoutGroup("$Name/Objects")]public GameObject spellObject;
			
		[HideInInspector]
		[BitMask(typeof(UISpellInfo_Flags))]
		public UISpellInfo_Flags Flags;
	}

}
