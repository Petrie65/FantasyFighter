using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager GM;
    public bool DEBUG = true;

    public GameObject playerUnit;

    public Player[] players;
    public int currentPlayerNum { get; set; }
    public Player currentPlayer { get; set; }

    public static Color[] playerColor = new Color[]
    {
        new Color(0F, 0F, 255F),    // Blue
        new Color(0F, 255F, 255F),  // Cyan
        new Color(255F, 0F, 255F),  // Purple
        new Color(255F, 255F, 0F),  // Yellow
        new Color(255F, 165F, 0F),  // Orange
        new Color(255F, 192F, 203F),// Pink
        new Color(220F, 220F, 220F),// Grey
        new Color(0F, 128F, 128F),  // Teal
        new Color(46F, 139F, 87F),  // Sea Green
        new Color(210F, 180F, 140F)// Tan
    };

    private void Awake() {
        MakeThisTheOnlyGameManager();
        players = new Player[10];

        //  instantiate players;
        for (int x = 0; x < players.Length; x++) {
            string name = "Player " + x.ToString();
            players[x] = new Player(name);

            GameObject currentUnit = Instantiate(playerUnit);
            currentUnit.transform.Translate(new Vector3(x * 5, 1, 0));

            currentUnit.GetComponent<UnitScript>().SetOwner(x);

            players[x].unit = currentUnit;
        }

        PlayerManager.PM.SetCurrentPlayer(0);
    }

	public Color getPlayerColor(int playerNum) {
		return playerColor[playerNum];
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

    public bool acquireItem(int player, string item) {
        for (int x = 0; x < 4; x++) {
            string spell = players[player].spells[x];
            if (spell == "") {
                players[player].spells[x] = item;
                Debug.Log("spell " + x.ToString() + " = " + item);
                GUIManager.GUI.updateGUI(player);
                return true;
            }
        }
        return false;
    }
}

public class Player {
    public GameObject unit;
    public int money;
    public string[] spells;
    public string name;

    public Player(string name) {
        this.name = name;
        spells = new string[4];
        for (int x = 0; x < spells.Length; x++) {
            spells[x] = "";
        }
    }
}