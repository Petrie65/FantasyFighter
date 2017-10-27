using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {
    public Player owner {get; set; }

	public Light unitLight;
    public GameObject unitMesh;

    public int selectedSpellIdx = 0;
    public Spell selectedSpell = null;

    [HideInInspector]
    public int maxHP;
    public int currentHP;
    public int mana;

    private Animator anim;
    private Rigidbody playerRigidbody;

    private int floorMask;
    private float camRayLength = 100f;

    public void Awake() {
        owner = null;

        currentHP = 100;
        maxHP = 100;
        mana = 100;

        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (selectedSpell != null) {
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
            SpellManager.SM.CastSpellMouse(owner.playerNum, selectedSpell, adjustedPoint);
            GetComponent<WizardMovement>().CastSpell(1, adjustedPoint);
			
        }
    }

    public bool SetOwner(Player player) {
        owner = player;
        name = "Wizard P" + owner.playerNum.ToString() + " (" +  GameManager.GM.colors.colorName[owner.playerNum] + ")";
        
        unitLight.color = GameManager.GM.colors.lightColor[owner.playerNum];

        unitMesh.GetComponent<SkinnedMeshRenderer>().material.mainTexture = Resources.Load("WizardSkin/wizardTexture" + owner.playerNum.ToString()) as Texture;
        return true;
    }

    public void TakeDamage(Player playerFrom, int damage) {
        if (playerFrom.name != owner.name) {
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
