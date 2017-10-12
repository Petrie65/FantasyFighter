using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform target { get; set; }

    public float smoothing = 5f;

    Vector3 offset;    

    void Start() {
        offset = transform.position - Vector3.zero;
    }

    void FixedUpdate() {
        if (target != null) {
            Vector3 targetCamPos = target.position + offset;
            
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}
