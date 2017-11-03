using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ObjectUI : MonoBehaviour {
    public float heightOffset;
    public float scaleDiv;

    public GameObject unitUI;
    
    GameObject[] wizards;
    UnitScript[] scripts;

    private List<GameObject> UnitUIList = new List<GameObject>();

    private GameObject[] UnitText;
    private GameObject[] UnitHealthBar;
    private GameObject[] UnitHealthBarHandle;

    private Animator[] animator;

    private float heightScale;
        
	void Awake () {
        wizards = GameObject.FindGameObjectsWithTag("Player");

        UnitText = new GameObject[wizards.Length];
        UnitHealthBar = new GameObject[wizards.Length];
        UnitHealthBarHandle = new GameObject[wizards.Length];
        animator = new Animator[wizards.Length];
        scripts = new UnitScript[wizards.Length];
	}

    private void Start() {
        // TODO: update scale whenever screen resizes
        float heightScale = (float)(Screen.height) / scaleDiv;
        heightOffset *= heightScale;

        for (int x = 0; x < wizards.Length; x++) {
            UnitUIList.Add(Instantiate(unitUI, this.transform));
            
            UnitText[x] = UnitUIList[x].transform.GetChild(0).gameObject;
            UnitHealthBar[x] = UnitUIList[x].transform.GetChild(1).gameObject;
            UnitHealthBarHandle[x] = UnitUIList[x].transform.GetChild(1).GetChild(0).GetChild(0).gameObject;

            animator[x] = UnitUIList[x].GetComponent<Animator>();

            Scrollbar hp = UnitHealthBar[x].GetComponent<Scrollbar>();
            ColorBlock cb = hp.colors;
            cb.disabledColor = GameManager.GM.colors.color[x];
            hp.colors = cb;

            UnitUIList[x].GetComponent<RectTransform>().localScale = new Vector3(heightScale,heightScale,heightScale);

            UnitText[x].GetComponent<Text>().text = wizards[x].GetComponent<UnitScript>().owner.name;
            scripts[x] = wizards[x].GetComponent<UnitScript>();
        }
    }
	
	void Update () {
        Vector3 playerPos;
        for (int x = 0; x < wizards.Length; x++) {
            GameObject unit = UnitUIList[x];
            // Position UI object on unit
            playerPos = Camera.main.WorldToScreenPoint(wizards[x].transform.position);
            playerPos.y += heightOffset;
            unit.transform.position = playerPos;

            // Set height offset
            Vector3 pos = UnitUIList[x].GetComponent<RectTransform>().position;
            pos.y += heightOffset;
            unit.GetComponent<RectTransform>().position = pos;
     
            // Update health bar
            float hpWidth = 150f * (scripts[x].currentHP / scripts[x].maxHP);
            UnitHealthBarHandle[x].GetComponent<RectTransform>().sizeDelta = new Vector2(hpWidth, 20);
        }
    }

    public void FadeObject(int num) {
		animator[num].SetTrigger("triggerFadeOut");
    }
}
