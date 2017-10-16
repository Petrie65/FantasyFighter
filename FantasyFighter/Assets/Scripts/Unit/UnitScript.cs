using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {
    public string ownerName { get; set; }
    public int ownerNum { get; set; }

	public Light unitLight;
    public GameObject unitMesh;

    public int hp { get; set; }
    public int mana { get; set; }

    public string selectedSpell = "";

    private Animator anim;
    private Rigidbody playerRigidbody;

    private int floorMask;
    private float camRayLength = 100f;

    public void Awake() {
        ownerName = "undefined";
        ownerNum = -1;
        hp = -1;
        mana = -1;

        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (selectedSpell != "") {
            if (Input.GetMouseButtonDown(0)) {
                CastSpellMouse(Input.mousePosition);
            }
        }
    }
    

    private void CastSpellMouse(Vector3 mousePos) {
        Ray camRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 adjustedPoint = new Vector3(floorHit.point.x, this.transform.position.y + 0.9f, floorHit.point.z);
            SpellManager.SM.CastSpellMouse(ownerNum, selectedSpell, adjustedPoint);

            GetComponent<WizardMovement>().CastSpell(0, adjustedPoint);
            Vector3 playerToMouse = adjustedPoint - transform.position;
            //playerToMouse.y = 0f;

            // Z axis is front
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = newRotation;
           // transform.Rotate()

          //  GetComponent<Rigidbody>().MoveRotation(newRotation);
        }




    }

    public bool SetOwner(int num) {
        ownerName = "Player " + num.ToString();
        name = "Wizard P" + num.ToString() + " (" +  GameManager.GM.colors.colorName[num] + ")";
        ownerNum = num;
        
        unitLight.color = GameManager.GM.colors.color[num];

        unitMesh.GetComponent<SkinnedMeshRenderer>().material.mainTexture = Resources.Load("WizardSkin/wizardTexture" + num.ToString()) as Texture;

        return true;
    }


}
