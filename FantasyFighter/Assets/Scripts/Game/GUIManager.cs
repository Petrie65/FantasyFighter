﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public static GUIManager GUI;
    public GameObject[] spellButtons;
    public GameObject playerHUD;
    public Text playerControl;

    private Text hudHP;
    private Text hudMana;
    private Text hudMoney;

    private void Awake() {
       MakeThisOnlyGUIManager();

       hudHP = playerHUD.transform.GetChild(0).gameObject.GetComponent<Text>();
       hudMana = playerHUD.transform.GetChild(1).gameObject.GetComponent<Text>();
       hudMoney = playerHUD.transform.GetChild(2).gameObject.GetComponent<Text>();
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

    public bool updateGUI(Player player) {
        if (GameManager.GM.currentPlayer == player) {
            // Spells
            for (int x = 0; x < spellButtons.Length; x++) {
                Spell spell = player.spells[x];
                bool isSpell = spell != null;

                Text spellButtonText = spellButtons[x].GetComponentInChildren<Text>();

                spellButtonText.text = isSpell ? spell.name : x.ToString();
                spellButtons[x].GetComponentInChildren<Button>().interactable = isSpell;
            }
            // HUD
            UnitScript unitScript = player.unit.GetComponent<UnitScript>();
            hudHP.text = unitScript.currentHP.ToString() + " / " + unitScript.maxHP.ToString();
            hudMana.text = unitScript.mana.ToString();
            hudMoney.text = player.money.ToString();
            return true;
        }
        return false;
    }
}
