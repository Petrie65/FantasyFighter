using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellInventory : MonoBehaviour {
    public GameObject[] spells;
    public GameObject spellButtons;

    private void Awake() {
        for (int x = 0; x < spells.Length; x++) {

            if (spells[x] != null) {
                var xx = spellButtons.transform.GetChild(x);
                xx.GetComponentInChildren<Text>().text = spells[x].name;
            }
            
        }
    }
}
