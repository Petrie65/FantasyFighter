using UnityEngine;
using System;
using DuloGames.UI;

public class Spell {
    public UISpellInfo Info;
    public Player Owner;

    public GameObject SpellObject;
    public SpellObject SpellScript;

    public bool canRelease = false;

    public float channelCounter = 0;
    public float chargeCounter = 0;
    public float holdCounter = 0;    

    private float ChargeDecayMult = 2f;
    private bool ChargeActive;

    public bool SpellFailed = false;

    private UnitScript unitScript;

    public Spell(Player owner, UISpellInfo info) {
        this.Owner = owner;
        this.Info = info;

        unitScript = owner.unit.GetComponent<UnitScript>();
    }

    public bool isActive() {
        return ChargeActive;
    }
    public void SetActive(bool status) {
        ChargeActive = status;
    }

    public void Update() {
        if (ChargeActive) {
            // Channel
            if (Info.ChannelTime >= channelCounter) {
                canRelease = false;
         
                // Only increase counter if mana can be drained
                if (unitScript.DrainMana(Info.PowerCost)) {
                    channelCounter += 1 * Time.deltaTime;
                }
                return;
            } 
            canRelease = true;

            // Charge
            if (Info.ChargeTo != 0) {
                if (Info.ChargeTo >= chargeCounter) {
                    chargeCounter += 1 * Time.deltaTime;
                    return;
                }
            }

            // Hold
            if (Info.HoldTo == 0) {
                return;
            } else if (Info.HoldTo >= holdCounter) {
                holdCounter += 1 * Time.deltaTime;
                return;
            }

            // Fail
            if (Info.ShouldFail) {
                holdCounter = 0f;
                chargeCounter = 0f;
                channelCounter = Info.ChannelTime * 0.75f;
                
                ChargeActive = false;
                unitScript.UnselectSpells();
                Debug.Log("Fail");
                return;
            }

            // Release
            Debug.Log("Realse");
            unitScript.ReleaseSpell();

        } else { /* Not active */
            // Reset Hold
            holdCounter = 0;

            // Decay charge
            if (chargeCounter > 0) {
                chargeCounter -= Info.DecayRate * ChargeDecayMult * Time.deltaTime;
                return;
            }

            // Decay channel
            if (channelCounter > 0) {
                channelCounter -= Info.DecayRate * Time.deltaTime;
                return;
            }
        }
    }

}
