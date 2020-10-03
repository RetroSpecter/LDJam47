using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boundary : MonoBehaviour {

    public UnityEvent triggerHit;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.TryGetComponent(out Player p)) {
            // Camera stuff to enter next area
            Debug.Log(this.name + " -> " + col.name);
            triggerHit.Invoke();

            // State stuff to enter the next area
            GameController.Instance.ProgressToNextArea();
        }
    }
}
