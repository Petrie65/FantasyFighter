using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpell : MonoBehaviour {
    public string spellName;
    public ParticleSystem[] particles;
    public Light areaLight;

    private float tweenSpeed = 5f;

    private bool pickedUp = false;
    private GameObject container;
    private GameObject pickupUnit;

    private void Awake() {
        container = transform.parent.gameObject;
    }

    private void Update() {
        if (pickedUp) {
            container.transform.position = Vector3.Lerp(container.transform.position, pickupUnit.transform.position, tweenSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(new Vector3(0.2f,0.2f,0.2f), new Vector3(0f, 0f, 0f), tweenSpeed * Time.deltaTime);
            if (areaLight.intensity > 0) {
                areaLight.intensity -= 0.2f;
                Debug.Log("in: " + areaLight.intensity.ToString());
            }

        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            if (GameManager.GM.acquireItem(collision.gameObject.GetComponent<UnitScript>().ownerNum, spellName)) {
                // Only destroy if item has been picked up
                GetComponent<SphereCollider>().enabled = false;

                pickupUnit = collision.gameObject;
                pickedUp = true;

                GetComponent<Animator>().enabled = false;

                for (int x=0;x<particles.Length;x++) {
                    if (particles[x]) {
                        particles[x].Stop();
                    }
                }

                StartCoroutine(ScaleOverTime(0.5f));
            }
        }
    }

    IEnumerator ScaleOverTime(float time) {
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(0f, 0f, 0f);

        float currentTime = 0.0f;

        do {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        Destroy(gameObject);
    }
}
