using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class GUIManager : MonoBehaviour {
    public static GUIManager GUI;
    public GameObject[] spellButtons;

    public GameObject playerHP;
    public GameObject playerMana;
    public GameObject playerStamina;

    public GameObject hpTextObject;
    public GameObject manaTextObject;
    
    public Text playerControl;

    private Text hpText;
    private Text manaText;

    private UIProgressBar hudHP;
    private UIProgressBar hudMana;
    private UIProgressBar hudStamina;

    private void Awake() {
       MakeThisOnlyGUIManager();
 
       hudHP = playerHP.GetComponent<UIProgressBar>();
       hudMana = playerMana.GetComponent<UIProgressBar>();
       hudStamina = playerStamina.GetComponent<UIProgressBar>();

       hpText = hpTextObject.GetComponent<Text>();
       manaText = manaTextObject.GetComponent<Text>();
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
                if (spell != null) {
                    spellButtons[x].GetComponent<UISpellSlot>().Assign(spell.GetInfo());
                } else {
                    spellButtons[x].GetComponent<UISpellSlot>().Unassign();
                }
            }
            // HUD
            UnitScript unitScript = player.unit.GetComponent<UnitScript>();
            hudHP.fillAmount =  unitScript.currentHP / unitScript.maxHP;
            hpText.text = unitScript.currentHP.ToString() + " / " + unitScript.maxHP.ToString();

            hudMana.fillAmount =  unitScript.currentMana /  unitScript.maxMana;
            manaText.text = unitScript.currentMana.ToString() + " / " + unitScript.maxMana.ToString();
            hudStamina.fillAmount =  unitScript.stamina / 100f;
            return true;
        }
        return false;
    }
}
