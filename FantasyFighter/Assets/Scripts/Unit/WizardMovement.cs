using UnityEngine;
using System.Collections;

public class WizardMovement : MonoBehaviour {
    public float walkSpeed = 5f;
    public float runSpeed = 7f;
    public float rotateSpeed = 80f;

    public float staminaRechargeDelay = 60f;

    public float dodgeSpeed;
    public float dodgeDistance;
    private float dodgeMoved = 0f;
    private float dodgeHeight = 10f;

    private Vector3 movement = new Vector3(0, 0, 0.1f);
    private Animator anim;
    private Rigidbody playerRigidbody;

    private bool isAnimating = false;
    private bool sprintActive = false;
    private bool isDodging = false; 
    
    private float staminaTimer;

    private UnitScript unitScript;

    // Awake always gets called
    // Start only gets called if script is enabled
    private void Awake() {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        unitScript = GetComponent<UnitScript>();

        staminaTimer = staminaRechargeDelay;
    }

    private void Update() {
        if (staminaTimer-- <= 0) {
            RegenStamina(0.4f);
        }
    }


    // Every physics update (you're moving a physics character with rigidbody attched)
    private void FixedUpdate() {
        if (GameManager.GM.currentPlayer != unitScript.owner) return;
        if (isAnimating || unitScript.isDead) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        sprintActive = Input.GetButton("Sprint");

        Move(h, v);
        Rotate();
        Animating(h, v);
    }

    private void Move(float h, float v) {
        if (h == 0f && v == 0f) { return; }

        // Unit is moving, reset timer
        staminaTimer = staminaRechargeDelay;

        if (Input.GetMouseButtonDown(1)) {
            if (UseStamina(50f)) {
                StartDodge(h, v);
                return;
            }
        }

        movement.Set(h, 0, v);

        if (sprintActive && unitScript.stamina > 0f) {
            movement = movement.normalized * runSpeed * Time.deltaTime;
            unitScript.stamina -= 0.7f;
            GUIManager.GUI.updateGUI(unitScript.owner);
        } else {
            movement = movement.normalized * walkSpeed * Time.deltaTime;
        }
        
        // Move a rigidbody to a position in world space
        playerRigidbody.transform.position += movement;
    }

    private void Rotate() {
        transform.rotation = Quaternion.Lerp(
            playerRigidbody.transform.rotation,
            Quaternion.LookRotation(movement),
            rotateSpeed * Time.deltaTime
        );
    }

    public void RegenStamina(float amount) {
        if (unitScript.stamina <= 100) {
            unitScript.stamina += amount;
            GUIManager.GUI.updateGUI(unitScript.owner);
        }
    }

    public bool UseStamina(float amount) {
        if (unitScript.stamina - amount >= 0) {
            unitScript.stamina -= amount;
            GUIManager.GUI.updateGUI(unitScript.owner);
            return true;
        } 
        return false;        
    }

	public void CastSpell(int animNum, Vector3 spellPos) {
		var animation = StartCoroutine(WaitForAnimation(animNum, spellPos));
	}

	private IEnumerator WaitForAnimation(int animNum, Vector3 pos) {
		isAnimating = true;

		anim.SetTrigger("triggerAttack" + animNum.ToString());
		
		Vector3 playerToMouse = pos - transform.position;
		playerToMouse.y = 0f;
		
		Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
		transform.rotation = newRotation;

		yield return new WaitForSeconds(0.5f); 

		isAnimating = false;
	}

    private void StartDodge(float h, float v) { 
        var dodgeForce = 800f;
        isDodging = true;
		anim.SetTrigger("triggerDodge");
        playerRigidbody.AddForce(new Vector3(h, 0.3f, v) * dodgeForce);
    }

	private void Animating(float h, float v) {
        if (h != 0f || v != 0f) {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", sprintActive);
        } else {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }
    }

}
