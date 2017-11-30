using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitScript : MonoBehaviour {
    public Player owner {get; set;}

	public Light unitLight;
    public GameObject unitMesh;

    public int selectedSpellIdx = 0;
    public Spell selectedSpell = null;
    public Spell activeSpell = null;

    public ParticleSystem staffParticles;

    public List<Buff> CurrentBuffs = new List<Buff>();

    [HideInInspector]
    public float currentHP;
    public float maxHP;
    public float regenHP;

    public float currentMana;
    public float maxMana;
    public float regenMana;

    public float stamina;

    private Animator anim;
    private Rigidbody unitRigidbody;

    private int floorMask;
    private float camRayLength = 100f;

    public bool isDead = false;

    public bool isCharging = false;
    public float castProgress = 0f;

    public void Awake() {
        currentHP = 100f;
        maxHP = 100f;
        regenHP = 0.05f;

        currentMana = 100f;
        maxMana = 100f;
        regenMana = 0.1f;

        stamina = 100f;

        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        unitRigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (!isDead) {
            // Regenerate hp and mana
            // GainHealth(regenHP);
            if (activeSpell == null || activeSpell.canRelease) {
                GainMana(regenMana);
            }

            // Handle player controls
            if (GameManager.GM.currentPlayer != owner) return;

            if (selectedSpell != null) {

                if (Input.GetMouseButton(0)) {
                    if (activeSpell != null && activeSpell.canRelease) {
                        CastSpellMouse(Input.mousePosition);
                        SpellManager.SM.castBar.ClearSpell();
                        activeSpell.SetActive(false);

                        UnselectSpells();
                        return;
                    }
                }

                if (Input.GetMouseButton(1)) {
                    if (activeSpell == null) {
                        ActivateSpell(selectedSpell);
                        return;
                    }
                }

                if (Input.GetMouseButtonUp(1)) {
                    DisableSpell();
                    return;
                }
            }
        } else {
            PerformDie();
        }
    }

    public void ActivateSpell(Spell spell) {
        activeSpell = spell;
        activeSpell.SetActive(true);

        SpellManager.SM.castBar.SetSpell(spell);
        SpellManager.SM.castBar.Show();
    }

    public void DisableSpell() {
        activeSpell.SetActive(false);
        activeSpell = null;

    }

    public void ReleaseSpell() {
        CastSpellMouse(Input.mousePosition);
        SpellManager.SM.castBar.ClearSpell();
        activeSpell.SetActive(false);

        UnselectSpells();
    }

    public void UnselectSpells() {
        selectedSpellIdx = 0;
        selectedSpell = null;
        activeSpell = null; // this might break
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

    public void GainHealth(float hp) {
        if (currentHP + hp > maxHP) {
            currentHP = maxHP;
        } else {
            currentHP += hp;
        }
    }

    public void GainMana(float mana) {
        if (currentMana + mana > maxMana) {
            currentMana = maxMana;
        } else {
            currentMana += mana;
        }
    }

    public void TakeDamage(Player playerFrom, float damage) {
        if (currentHP - damage < 0) {
            currentHP = 0;

            if (!isDead) {
                isDead = true;
                ConsoleProDebug.LogToFilter("Killed by " + playerFrom.name, "Unit");
                StartCoroutine(DieRoutine());
            }
        } else {
            currentHP -= damage;

        }
    }

    public bool DrainMana(float amount) {
        if (amount > currentMana) {
            return false;
            // TODO: Notify player not enough mana
        } else {
            currentMana -= amount;
            return true;
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
        GameManager.GM.objectUIScript.FadeUnitUI(owner.playerNum);
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

    public Buff AddBuff<T>(Player buffOwner, float duration, int stacks, float intensity) where T : Buff{
        System.Type buffType = typeof(T);

        var currentBuff = GetBuff(buffType.ToString());
        if (currentBuff == null) {
            // Unit does not have buff yet
            Component buff = gameObject.AddComponent(buffType);
            Buff buffScript = buff.GetComponent<Buff>();
            buffScript.Init(this, buffOwner, duration, stacks, intensity);

            CurrentBuffs.Add(buffScript);
            return buffScript;
        } else {
            // Buff already exists
            if (currentBuff.Info.Stackable) {
                currentBuff.AddStack();
            } else {
                currentBuff.ResetTime();
            }

            return null;
        }
    }

    public void RemoveBuff(Buff buff) {
        var currentBuff = GetBuff(buff.Info.Name);
        if (currentBuff != null) {
            CurrentBuffs.Remove(buff);
            
        }
    }

    public Buff GetBuff(string buffName) {
        foreach(Buff has in CurrentBuffs.ToArray()) {
            if (has.Info.Name == buffName) {
                return has;
            }
        }
        return null;
    }
}
