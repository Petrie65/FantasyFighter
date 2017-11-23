﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

public class WorldSpell : MonoBehaviour {
    public string spellName;
    public ParticleSystem[] particles;
    public Light areaLight;

    private float tweenSpeed = 5f;

    private bool pickedUp = false;
    private GameObject pickupUnit;

    [HideInInspector]
    // The worldspell should have a reference to it's associated UI object
    public GameObject spellUI;

    private void Awake() {
        
    }

    private void Update() {
        if (pickedUp) {
            this.transform.position = Vector3.Lerp(this.transform.position, pickupUnit.transform.position, tweenSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(new Vector3(0.2f,0.2f,0.2f), new Vector3(0f, 0f, 0f), tweenSpeed * Time.deltaTime);
            if (areaLight.intensity > 0) {
                areaLight.intensity -= 0.1f;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (GameManager.GM.acquireSpell(other.gameObject.GetComponent<UnitScript>().owner, spellName)) {
                // Only destroy if item has been picked up
                GetComponent<SphereCollider>().enabled = false;

                pickupUnit = other.gameObject;
                pickedUp = true;

                transform.GetChild(0).GetComponent<Animator>().enabled = false;

                for (int x=0;x<particles.Length;x++) {
                    if (particles[x]) {
                        particles[x].Stop();
                    }
                }
                
                spellUI.GetComponent<Animator>().SetTrigger("triggerFadeOut");

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

        SpellManager.SM.RemoveWorldSpell(gameObject);
    }
}
