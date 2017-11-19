using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

public class GameManager : MonoBehaviour {
    public static GameManager GM;
    public bool DEBUG = true;

    public GameObject playerUnit;

    public Player[] players;
    public Player currentPlayer { get; set; }

    public GameObject[] startPositions;

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

            currentUnit.transform.position = startPositions[x].transform.position;
            Destroy(startPositions[x]);

            players[x].playerNum = x;
            currentUnit.GetComponent<UnitScript>().SetOwner(players[x]);
            players[x].unit = currentUnit;
        }

    }

    private void Start() {
        PlayerManager.PM.SetCurrentPlayer(players[0]);
        objectUIScript = objectUI.GetComponent<ObjectUI>();
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

    private void Update() {
        foreach(Player player in players) {
            if (player != null)
                player.Update();
        }
    }

    // public bool acquireItem(Player player, Spell spell) {
    //     for (int x = 0; x < 4; x++) {
    //         Spell currentSpell = player.spells[x];
    //         if (currentSpell == null) {
    //             player.spells[x] = spell;
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    public bool acquireSpell(Player player, string spellName) {
        UISpellInfo spell = UISpellDatabase.Instance.GetByName(spellName);
        if (spell == null) {
            Debug.Log("Spell doesnt exist");
            return false;
        }

        for (int x = 0; x < 5; x++) {
            Spell currentSpell = player.spells[x];
            if (currentSpell == null) {
                player.spells[x] = new Spell(player, spell);
                return true;
            }
        }
        return false;
    }
}