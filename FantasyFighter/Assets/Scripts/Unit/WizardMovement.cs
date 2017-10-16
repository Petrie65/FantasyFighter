using UnityEngine;

public class WizardMovement : MonoBehaviour {
    public float walkSpeed = 5f;
    public float runSpeed = 7f;
    public float rotateSpeed = 80f;

    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;

    private bool isAnimating = false;
    private bool sprintActive = false;
    
    private UnitScript unitScript;

    // Awake always gets called
    // Start only gets called if script is enabled
    private void Awake() {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();

        unitScript = GetComponent<UnitScript>();
    }

    // Every physics update (you're moving a physics character with rigidbody attched)
      private void FixedUpdate() {
        if (GameManager.GM.currentPlayer.name != unitScript.ownerName) return;
        if (isAnimating) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        sprintActive = Input.GetButton("Sprint");

        Move(h, v);
        Rotate();
        Animating(h, v);
    }

    private void Move(float h, float v) {
        if (h == 0f && v == 0f) return;

        movement.Set(h, 0, v);

        movement = movement.normalized * (sprintActive ? runSpeed : walkSpeed) * Time.deltaTime;

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

    public void CastSpell(int animNum, Vector3 spellPos) {
        isAnimating = true;
        anim.SetTrigger("triggerAttack1");
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
