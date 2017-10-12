using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {
    public string ownerName { get; set; }
    public int ownerNum { get; set; }

    public int hp { get; set; }
    public int mana { get; set; }

    public void Awake() {
        ownerName = "undefined";
        ownerNum = -1;
        hp = -1;
        mana = -1;
    }

    public bool SetOwner(int num) {
        ownerName = "Player " + num.ToString();
        ownerNum = num;
        return true;
    }
}
