using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class GUIManager : MonoBehaviour {
    public static GUIManager GUI;
    public GameObject[] spellButtons;
    public GameObject playerHUD;

    public GameObject playerHP;
    public GameObject playerMana;
    public GameObject playerStamina;
    
    public Text playerControl;

    private UIProgressBar hudHP;
    private UIProgressBar hudMana;
    private UIProgressBar hudStamina;
    private Text hudMoney;

    private void Awake() {
       MakeThisOnlyGUIManager();

    //    hudHP = playerHUD.transform.GetChild(0).gameObject.GetComponent<Text>();
    //    hudMana = playerHUD.transform.GetChild(1).gameObject.GetComponent<Text>();
    //    hudMoney = playerHUD.transform.GetChild(2).gameObject.GetComponent<Text>();       
    
       hudHP = playerHP.GetComponent<UIProgressBar>();
       hudMana = playerMana.GetComponent<UIProgressBar>();
       hudStamina = playerStamina.GetComponent<UIProgressBar>();
       
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
            hudHP.fillAmount =  (float)unitScript.currentHP / (float)unitScript.maxHP;
            // hudHP.text = unitScript.currentHP.ToString() + " / " + unitScript.maxHP.ToString();
            hudMana.fillAmount =  (float)unitScript.mana / 100f;
            hudStamina.fillAmount =  (float)unitScript.stamina / 100f;
            //hudMana.text = unitScript.mana.ToString();
            hudMoney.text = player.money.ToString();
            return true;
        }
        return false;
    }
}
