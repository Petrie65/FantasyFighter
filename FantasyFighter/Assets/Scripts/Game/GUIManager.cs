using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public static GUIManager GUI;
    public GameObject[] spellButtons;
    public Text playerControl;

    private void Awake() {
       MakeThisOnlyGUIManager();
    }
    void MakeThisOnlyGUIManager() {
        if (GUI == null) {
            DontDestroyOnLoad(gameObject);
            GUI = this;
        } else {
            if (GUI != this) {
                Destroy(gameObject);
            }
        }
    }

    public bool updateGUI(int player) {
        if (GameManager.GM.currentPlayerNum == player) {
            for (int x = 0; x < spellButtons.Length; x++) {
                Spell spell = GameManager.GM.players[player].spells[x];
                bool isSpell = spell != null;

                Text spellButtonText = spellButtons[x].GetComponentInChildren<Text>();

                spellButtonText.text = isSpell ? spell : x.ToString();
                spellButtons[x].GetComponentInChildren<Button>().interactable = isSpell;
            }
            return true;

            // TODO: Update health, mana, money

        }
        return false;
    }
}
