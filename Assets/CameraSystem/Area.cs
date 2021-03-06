﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

    public int areaNum;

    private HashSet<GameObject> queuedForLeaving;
    private HashSet<EnterAreaEvent> queuedForEntering;

    public delegate void EnterAreaEvent();

    public Collider2D enterCollider;
    public Collider2D exitCollider;

    // Start is called before the first frame update
    protected void Start() {
        GameController.Instance.RegisterArea(this.areaNum, this);
        this.queuedForLeaving = new HashSet<GameObject>();
        this.queuedForEntering = new HashSet<EnterAreaEvent>();
    }

    // On the event that the player leaves the area, do the following:
    //  - Disable all gameObjects that have queued themselves to leave the area
    public void PlayerLeft() {
        foreach (GameObject go in this.queuedForLeaving) {
            go.SetActive(false);
        }
        queuedForLeaving.Clear();

        enterCollider.isTrigger = true;
        exitCollider.isTrigger = false;

        StartCoroutine(SwitchColliders(false));
    }

    public void PlayerEntered() {
        foreach (EnterAreaEvent areaEvent in this.queuedForEntering) {
            areaEvent();
        }
        queuedForLeaving.Clear();

        CameraRotationManager.instance.RotateCameraTo(this.transform);
        enterCollider.isTrigger = false;
        exitCollider.isTrigger = true;

        StartCoroutine(SwitchColliders(true));
    }

    //TODO: this is pretty ugly. Ther is also a corner case that can cause the player to get stuck behind a barrier
    IEnumerator SwitchColliders(bool on)
    {
        yield return new WaitForSeconds(0.25f);
        enterCollider.gameObject.layer = on ? 8 : 0;
        exitCollider.gameObject.layer = on ? 0 : 8;
    }

    // Given an interactable object,
    public void AddToLeavingQueue(GameObject go) {
        this.queuedForLeaving.Add(go);
    }

    // Given an event method, queues it to be called when you enter an area
    public void AddToEnteringQueue(EnterAreaEvent areaEvent) {
        this.queuedForEntering.Add(areaEvent);
    }
}
