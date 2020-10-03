using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

    public GameObject cam;
    public GameObject area;


    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Bleg");
        this.cam.transform.position = area.transform.position;
        this.cam.transform.rotation = Quaternion.Euler(0, 0, -45) * cam.transform.rotation;
    }
}
