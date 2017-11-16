using UnityEngine;
using System;
using DuloGames.UI;

public class Spell {
    public UISpellInfo Info;
    public Player Owner;

    public Spell(Player owner, UISpellInfo info) {
        this.Owner = owner;
        this.Info = info;
    }
}
