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

    private Player currentPlayer;

    private void Awake() {
       MakeThisOnlyGUIManager();
 
       hudHP = playerHP.GetComponent<UIProgressBar>();
       hudMana = playerMana.GetComponent<UIProgressBar>();
       hudStamina = playerStamina.GetComponent<UIProgressBar>();

       hpText = hpTextObject.GetComponent<Text>();
       manaText = manaTextObject.GetComponent<Text>();
    }

    private void Start() {
       this.currentPlayer = GameManager.GM.currentPlayer;
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

    public void SetCurrentPlayer(Player player) {
        this.currentPlayer = player;
    }

    void Update() {
        updateGUI(currentPlayer);
    }

    private void updateGUI(Player player) {
        // Spells
        for (int x = 0; x < spellButtons.Length; x++) {
            Spell spell = player.spells[x];
            if (spell != null) {
                spellButtons[x].GetComponent<UISpellSlot>().Assign(spell.Info);
            } else {
                spellButtons[x].GetComponent<UISpellSlot>().Unassign();
            }
        }
        // HUD
        UnitScript unitScript = player.unit.GetComponent<UnitScript>();
        hudHP.fillAmount =  unitScript.currentHP / unitScript.maxHP;
        hpText.text = Mathf.Floor(unitScript.currentHP).ToString() + " / " + Mathf.Floor(unitScript.maxHP).ToString();

        hudMana.fillAmount =  unitScript.currentMana /  unitScript.maxMana;
        manaText.text = Mathf.Floor(unitScript.currentMana).ToString() + " / " + Mathf.Floor(unitScript.maxMana).ToString();

        hudStamina.fillAmount =  unitScript.stamina / 100f;
    }

    public void UpdateSpells() {
        
    }
}
