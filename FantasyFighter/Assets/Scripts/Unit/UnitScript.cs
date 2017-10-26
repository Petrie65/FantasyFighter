using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {
    public string ownerName { get; set; }
    public int ownerNum { get; set; }

	public Light unitLight;
    public GameObject unitMesh;

    public int selectedSpellIdx = 0;
    public string selectedSpell = "";

    [HideInInspector]
    public int maxHP;
    public int currentHP;
    public int mana;

    private Animator anim;
    private Rigidbody playerRigidbody;

    private int floorMask;
    private float camRayLength = 100f;

    public void Awake() {
        ownerName = "undefined";
        ownerNum = -1;

        currentHP = 75;
        maxHP = 100;
        mana = 100;

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
            GetComponent<WizardMovement>().CastSpell(1, adjustedPoint);
			
        }
    }

    public bool SetOwner(int num) {
        ownerName = "Player " + num.ToString();
        name = "Wizard P" + num.ToString() + " (" +  GameManager.GM.colors.colorName[num] + ")";
        ownerNum = num;
        
        unitLight.color = GameManager.GM.colors.lightColor[num];

        unitMesh.GetComponent<SkinnedMeshRenderer>().material.mainTexture = Resources.Load("WizardSkin/wizardTexture" + num.ToString()) as Texture;
        return true;
    }

    public void TakeDamage(Player playerFrom, int damage) {
        if (playerFrom.name != ownerName) {
            currentHP -= damage;
            if (currentHP <= 0) {
                Debug.Log("Killed by " + playerFrom.name);
                Die();
            }
        }
    }

    public void Die() {

    }
}
