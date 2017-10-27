using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager GM;
    public bool DEBUG = true;

    public GameObject playerUnit;

    public Player[] players;
    public Player currentPlayer { get; set; }

    public ColorScript colors;

    public GameObject objectUI;

    [HideInInspector]
    public ObjectUI objectUIScript;

    private void Awake() {
        MakeThisTheOnlyGameManager();
        players = new Player[10];

        //  instantiate players;
        for (int x = 0; x < players.Length; x++) {
            string name = "Player " + x.ToString();
            players[x] = new Player(name);

            GameObject currentUnit = Instantiate(playerUnit);
            currentUnit.transform.Translate(new Vector3(x * 5, 1, 0));

            players[x].playerNum = x;
            currentUnit.GetComponent<UnitScript>().SetOwner(players[x]);

            players[x].unit = currentUnit;
        }

        objectUIScript = objectUI.GetComponent<ObjectUI>();

        PlayerManager.PM.SetCurrentPlayer(players[0]);
    }

    void MakeThisTheOnlyGameManager() {
        if (GM == null) {
            DontDestroyOnLoad(gameObject);
            GM = this;
        } else {
            if (GM != this) {
                Destroy(gameObject);
            }
        }
    }

    public bool acquireItem(Player player, Spell spell) {
        for (int x = 0; x < 4; x++) {
            Spell currentSpell = player.spells[x];
            if (currentSpell == null) {
                player.spells[x] = spell;
                GUIManager.GUI.updateGUI(player);
                return true;
            }
        }
        return false;
    }
}