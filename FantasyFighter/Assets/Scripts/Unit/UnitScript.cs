using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {
    public string ownerName { get; set; }
    public int ownerNum { get; set; }

	public Light unitLight;
    public GameObject unitMesh;

    public int hp { get; set; }
    public int mana { get; set; }

    public void Awake() {
        ownerName = "undefined";
        ownerNum = -1;
        hp = -1;
        mana = -1;
    }

    public bool SetOwner(int num) {
        ownerName = "Player " + num.ToString();
        name = "Wizard P" + num.ToString() + " (" +  GameManager.GM.colors.colorName[num] + ")";
        ownerNum = num;
        
        unitLight.color = GameManager.GM.colors.color[num];
       // unitLight.intensity = GameManager.GM.colors.intensity[num];

        unitMesh.GetComponent<SkinnedMeshRenderer>().material.mainTexture = Resources.Load("WizardSkin/wizardTexture" + num.ToString()) as Texture;

        return true;
    }
}
