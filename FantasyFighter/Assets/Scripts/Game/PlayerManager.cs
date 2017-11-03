using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager PM;

    public Text playerText;

    private CameraScript camScript;

    private void Awake() {
        MakeThisOnlyPlayerManager();

        camScript = Camera.main.GetComponent<CameraScript>();
    }

    void MakeThisOnlyPlayerManager() {
        if (PM == null) {
            DontDestroyOnLoad(gameObject);
            PM = this;
        } else {
            if (PM != this) {
                Destroy(gameObject);
            }
        }
    }

    public void SetCurrentPlayer(Player player) {
        if (player != null) {
            GameManager.GM.currentPlayer = player;
            GUIManager.GUI.SetCurrentPlayer(player);
            playerText.text = player.name;
            camScript.target = player.unit.transform;
        } else {
            Debug.Log("Player doesnt exist");
        }
    }

    public void SetCurrentPlayer(int playerNum) {
        SetCurrentPlayer(GameManager.GM.players[playerNum]);
    }
}
