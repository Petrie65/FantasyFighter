using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ObjectUI : MonoBehaviour {
    public float heightOffset;
    public float scaleDiv;

    public GameObject unitUI;
    public GameObject spellUI;
    
    GameObject[] wizards;
    UnitScript[] scripts;

    // GameObject[] worldSpells;
    private List<GameObject> worldSpells = new List<GameObject>();

    private List<GameObject> UnitUIList = new List<GameObject>();
    private GameObject[] UnitText;
    private GameObject[] UnitHealthBar;
    private GameObject[] UnitHealthBarHandle;
    
    private List<GameObject> SpellUIList = new List<GameObject>();
    private GameObject[] SpellText;

    private Animator[] unitAnimator;
    private Animator[] spellAnimator;

    private float heightScale;
        
	void Awake () {
        wizards = GameObject.FindGameObjectsWithTag("Player");

        UnitText = new GameObject[wizards.Length];
        UnitHealthBar = new GameObject[wizards.Length];
        UnitHealthBarHandle = new GameObject[wizards.Length];
        unitAnimator = new Animator[wizards.Length];
        scripts = new UnitScript[wizards.Length];

        worldSpells = SpellManager.SM.worldSpells;

        // TODO: update scale whenever screen resizes
        heightScale = (float)(Screen.height) / scaleDiv;
        heightOffset *= heightScale;
	}

    private void Start() {
        for (int x = 0; x < wizards.Length; x++) {
            UnitUIList.Add(Instantiate(unitUI, this.transform));
            
            UnitText[x] = UnitUIList[x].transform.GetChild(0).gameObject;
            UnitHealthBar[x] = UnitUIList[x].transform.GetChild(1).gameObject;
            UnitHealthBarHandle[x] = UnitUIList[x].transform.GetChild(1).GetChild(0).GetChild(0).gameObject;

            unitAnimator[x] = UnitUIList[x].GetComponent<Animator>();

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

        Vector3 spellPos;
        foreach (GameObject worldSpell in worldSpells){
            GameObject spellUI = worldSpell.GetComponent<WorldSpell>().spellUI;
            
            // Position UI object on unit
            spellPos = Camera.main.WorldToScreenPoint(worldSpell.transform.position);
            spellPos.y += heightOffset;
            spellUI.transform.position = spellPos;
        }
    }

    public void AddSpellUI(GameObject worldSpell) {
        WorldSpell spellScript = worldSpell.GetComponent<WorldSpell>();

        GameObject spellUI = Instantiate(this.spellUI, this.transform);
        spellUI.transform.GetChild(0).gameObject.GetComponent<Text>().text = spellScript.spellName;
        spellUI.GetComponent<RectTransform>().localScale = new Vector3(heightScale,heightScale,heightScale);

        SpellUIList.Add(spellUI);

        // Set the Gameobject's script to have a reference to the UI object
        spellScript.spellUI = spellUI;
    }

    public void RemoveSpellUI(GameObject WorldSpell) {
        SpellUIList.Remove(WorldSpell);
    }

    public void FadeUnitUI(int num) {
		unitAnimator[num].SetTrigger("triggerFadeOut");
    }
}
