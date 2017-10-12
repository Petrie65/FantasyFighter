using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpell : MonoBehaviour {
    public string spellName;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            if (GameManager.GM.acquireItem(collision.gameObject.GetComponent<UnitScript>().ownerNum, spellName)) {
                // Only destroy if item has been picked up
                Destroy(gameObject);
            }
        }
    }
}
