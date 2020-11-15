using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsate : MonoBehaviour {

    public float minScale;
    public float maxScale;
    public float speed;

    void Update() {
        transform.localScale += Vector3.one * speed * Time.deltaTime;
        if (transform.localScale.x > maxScale || transform.localScale.x < minScale) {
            speed *= -1;
        }
    }
}
