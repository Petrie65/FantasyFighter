using UnityEngine;
using System;

public class Spell {
    public string name;
    public int manaCost;
    public GameObject gameObject;
    public int iconNumber;

    public Player owner;

    public Spell(string name, int manaCost, GameObject gameObject, int iconNumber) {
        this.name = name;
        this.manaCost = manaCost;
        this.gameObject = gameObject;
        this.iconNumber = iconNumber;
    }
}
