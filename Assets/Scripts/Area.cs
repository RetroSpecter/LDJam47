using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

    public int areaNum;

    private HashSet<GameObject> queuedForLeaving;

    // Start is called before the first frame update
    protected void Start() {
        GameController.Instance.RegisterArea(this.areaNum, this);
        this.queuedForLeaving = new HashSet<GameObject>();
    }

    // On the event that the player leaves the area, do the following:
    //  - Disable all gameObjects that have queued themselves to leave the area
    public void PlayerLeft() {
        foreach (GameObject go in this.queuedForLeaving) {
            go.SetActive(false);
        }
    }

    public void PlayerEntered() {

    }


    // Given an interactable object,
    public void AddToLeavingQueue(GameObject go) {
        this.queuedForLeaving.Add(go);
    }
}
