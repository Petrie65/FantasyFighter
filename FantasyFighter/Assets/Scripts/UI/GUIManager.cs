using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class GUIManager : MonoBehaviour {
    public static GUIManager GUI;
    public GameObject[] spellButtons;
    public CanvasGroup[] spellHighlight;

    public Texture2D cursorTexture;

    public GameObject playerHP;
    public GameObject playerMana;
    public GameObject playerStamina;

    public CastBar castBar;

    public GameObject hpTextObject;
    public GameObject manaTextObject;
    
    public Text playerControl;

    private Text hpText;
    private Text manaText;

    private UIProgressBar hudHP;
    private UIProgressBar hudMana;
    private UIProgressBar hudStamina;

    private Player currentPlayer;
    private UnitScript unitScript;

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
        unitScript = this.currentPlayer.unit.GetComponent<UnitScript>();

       Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
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
        unitScript = player.unit.GetComponent<UnitScript>();
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

        if (unitScript == null) { return; }

        if (unitScript.selectedSpell != null) {
            HighlightButton(unitScript.selectedSpellIdx);
        } else {
            HighlightOff();
        }

        // HUD
        hudHP.fillAmount =  unitScript.currentHP / unitScript.maxHP;
        hpText.text = Mathf.Floor(unitScript.currentHP).ToString() + " / " + Mathf.Floor(unitScript.maxHP).ToString();

        hudMana.fillAmount =  unitScript.currentMana /  unitScript.maxMana;
        manaText.text = Mathf.Floor(unitScript.currentMana).ToString() + " / " + Mathf.Floor(unitScript.maxMana).ToString();

        hudStamina.fillAmount =  unitScript.stamina / 100f;

        if (unitScript.selectedSpell != null) {
            float channel = unitScript.selectedSpell.channelCounter / unitScript.selectedSpell.Info.ChannelTime;
            castBar.SetChannelAmount(channel);
            float charge = unitScript.selectedSpell.chargeCounter / unitScript.selectedSpell.Info.ChargeTo;
            castBar.SetChargeAmount(charge);
        }
    }

    public void HighlightButton(int btnIndex) {
        for (int i = 0; i < spellHighlight.Length; i++) {
            spellHighlight[i].alpha = (i == btnIndex) ? 1 : 0;
        }
    }

    public void HighlightOff() {
        for (int i = 0; i < spellHighlight.Length; i++) {
            spellHighlight[i].alpha = 0;
        }
    }
}
