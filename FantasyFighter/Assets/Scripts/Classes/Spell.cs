using UnityEngine;
using System;
using DuloGames.UI;

public class Spell {
    public int ID;
    public string Name;
    public Sprite Icon;

    public float Range;
    public float Cooldown;
    public float CastTime;
    public float PowerCost;

    public GameObject gameObject;
    public string Description;

    // Aditional vars
    public float Damage;
    public float HoldTime;
    public string CastType;
    public int Amount;
    // public GameObject worldObject;

    
    public Player owner;

    public Spell(int id, string name, Sprite icon, float range, float cooldown, float casttime, float powercost, GameObject gameObject, string description, float damage, float holdTime, string castType, int amount) {
        this.ID = id;
        this.Name = name;
        this.Icon = icon;
        this.Range = range;
        this.Cooldown = cooldown;
        this.CastTime = casttime;
        this.PowerCost = powercost;
        this.gameObject = gameObject;
        this.Description = description;

        this.Damage = damage;
        this.HoldTime = holdTime;
        this.CastType = castType;
        this.Amount = amount;
    }

    public UISpellInfo GetInfo() {
        UISpellInfo info = new UISpellInfo();
		info.ID = this.ID;
		info.Name = this.Name;
		info.Icon = this.Icon;
		info.Description = this.Description;
		info.Range = this.Range;
		info.Cooldown = this.Cooldown;
		info.CastTime = this.CastTime;
		info.PowerCost = this.PowerCost;

		info.Damage = this.Damage;
		info.HoldTime = this.HoldTime;
		info.CastType = this.CastType;
		info.Amount = this.Amount;
        
        return info;
    }
}
