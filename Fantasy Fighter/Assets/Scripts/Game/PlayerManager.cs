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

    public void SetCurrentPlayer(int playerNum) {
        if (GameManager.GM.players[playerNum] != null) {
            GameManager.GM.currentPlayerNum = playerNum;
            GameManager.GM.currentPlayer = GameManager.GM.players[playerNum];

            GUIManager.GUI.updateGUI(playerNum);

            playerText.text = GameManager.GM.currentPlayer.name;

            camScript.target = GameManager.GM.players[playerNum].unit.transform;
        } else {
            Debug.Log("Player doesnt exist");
        }
    }
}
