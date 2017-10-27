using UnityEngine;
using System;

public class Spell {
    public string name;
    public int manaCost;
    public GameObject gameObject;

    public Player owner;

    public Spell(string name, int manaCost, GameObject gameObject) {
        this.name = name;
        this.manaCost = manaCost;
        this.gameObject = gameObject;
    }
}
