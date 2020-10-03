using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

    public GameObject cam;


    void OnTriggerEnter2D(Collider2D col) {
        // Camera stuff to enter next area

        // State stuff to enter the next area
        GameController.Instance.ProgressToNextArea();
    }
}
