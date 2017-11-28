using UnityEngine;
using UnityEditor;
using System;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;

namespace DuloGames.UI
{
	[Serializable]
	public class UIBuffInfo
	{
		[FoldoutGroup("$Name")] public int ID;
		[FoldoutGroup("$Name")] public string Name;
		[FoldoutGroup("$Name")] public Sprite Icon;
		[FoldoutGroup("$Name")] public Color Color;
		[FoldoutGroup("$Name")] public string Description;

		[FoldoutGroup("$Name/Buff")] public bool IsPositive;
		[FoldoutGroup("$Name/Buff")] public bool Stackable;
		[FoldoutGroup("$Name/Buff"), EnableIf("Stackable")] public int MaxStack;
		[InlineEditor(InlineEditorModes.SmallPreview)]
		[FoldoutGroup("$Name/Buff")] public GameObject buffObject;	
	}

}
