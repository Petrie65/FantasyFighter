using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectUI : MonoBehaviour {
    public int heightOffset = 40;
    public GameObject UnitText;
    
    GameObject[] wizards;
    private List<GameObject> playerNameText = new List<GameObject>();
        
	void Awake () {
        wizards = GameObject.FindGameObjectsWithTag("Player");


        for (int x = 0; x < wizards.Length; x++) {

            playerNameText.Add(Instantiate(UnitText, this.transform));

            playerNameText[x].GetComponent<Text>().text = wizards[x].GetComponent<UnitScript>().ownerName;
        }
	}
	
	void Update () {
        Vector3 playerPos;
        for (int x = 0; x < wizards.Length; x++) {
            playerPos = Camera.main.WorldToScreenPoint(wizards[x].transform.position);
            playerPos.y += heightOffset;
            playerNameText[x].transform.position = playerPos;
        }
    }
}
