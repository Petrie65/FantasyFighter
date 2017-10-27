using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    public GameObject unit;
    public int playerNum;
    public int money;
    public string name;

    // TODO setup player color
    public Color playerColor;

    public Spell[] spells;

    public Player(string name) {
        this.name = name;

        spells = new Spell[4] {
            null, null, null, null
        };
    }
}