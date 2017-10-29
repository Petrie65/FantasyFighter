using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {
    public Player owner {get; set; }

	public Light unitLight;
    public GameObject unitMesh;

    public int selectedSpellIdx = 0;
    public Spell selectedSpell = null;

    public ParticleSystem staffParticles;

    [HideInInspector]
    public int maxHP;
    public int currentHP;
    public int mana;
    public int stamina;

    private Animator anim;
    private Rigidbody unitRigidbody;

    private int floorMask;
    private float camRayLength = 100f;

    public bool isDead = false;

    public void Awake() {
        owner = null;

        currentHP = 100;
        maxHP = 100;
        mana = 100;
        stamina = 100;

        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        unitRigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (!isDead) {
            if (GameManager.GM.currentPlayer != owner) return;

            if (Input.GetButtonDown("Fire1")) {
                if (selectedSpell != null) {
                    CastSpellMouse(Input.mousePosition);
                }
            }
        } else {
            PerformDie();
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
                StartCoroutine(DieRoutine());
            }
        }
    }

    string dieState;
    private IEnumerator DieRoutine() {
        isDead = true;
		anim.SetTrigger("triggerDie");
        
        // Prevent the body from falling
        unitRigidbody.useGravity = false;
        GetComponent<CapsuleCollider>().enabled = false;

        staffParticles.Stop();

        dieState = "animate";
        yield return new WaitForSeconds(1f);
        dieState = "light";
        yield return new WaitForSeconds(4f);
        dieState = "dissolve";
        yield return new WaitForSeconds(5f);
        dieState = "dead";
        
        gameObject.SetActive(false);
    }

    float dissolveSpeed = 0.3f;
    private void PerformDie() {
        switch (dieState) {
            case "animate":
                break;
            case "light":
                unitLight.intensity -= 7f * Time.deltaTime;
                break;
            case "dissolve":
                transform.Translate(Vector3.down * Time.deltaTime * dissolveSpeed, Space.World);
                break;
        }

    }
}
