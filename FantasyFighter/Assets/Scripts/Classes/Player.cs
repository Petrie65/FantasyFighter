using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    public GameObject unit;
    public int playerNum;
    public int money;
    public string name;

    // public string[] spells;
    public Spell[] spells

    public Player(string name) {
        this.name = name;

        // spells = new string[4];
        // for (int x = 0; x < spells.Length; x++) {
        //     spells[x] = "";
        // }

        spells = new Spell[4] {
            null, null, null, null
        };
    }
}