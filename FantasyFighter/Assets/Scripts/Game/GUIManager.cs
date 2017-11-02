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
    
    public Text playerControl;

    private UIProgressBar hudHP;
    private UIProgressBar hudMana;
    private UIProgressBar hudStamina;

    private void Awake() {
       MakeThisOnlyGUIManager();
 
       hudHP = playerHP.GetComponent<UIProgressBar>();
       hudMana = playerMana.GetComponent<UIProgressBar>();
       hudStamina = playerStamina.GetComponent<UIProgressBar>();
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

                if (isSpell) {
                    Sprite icon = SpellManager.SM.spellIcons[spell.iconNumber];
                    spellButtons[x].GetComponent<UISpellSlot>().SetIcon(icon);

                }
            }
            // HUD
            UnitScript unitScript = player.unit.GetComponent<UnitScript>();
            hudHP.fillAmount =  (float)unitScript.currentHP / (float)unitScript.maxHP;
            hudMana.fillAmount =  (float)unitScript.mana / 100f;
            hudStamina.fillAmount =  (float)unitScript.stamina / 100f;
            return true;
        }
        return false;
    }
}
