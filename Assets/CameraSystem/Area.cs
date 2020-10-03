using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

    public int areaNum;

    private HashSet<GameObject> queuedForLeaving;

    public Collider2D enterCollider;
    public Collider2D exitCollider;

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

        enterCollider.isTrigger = true;
        exitCollider.isTrigger = false;

        StartCoroutine(SwitchColliders(false));
    }

    public void PlayerEntered() {
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
        Debug.Log("registered an object to dissapear on leaving scene");
        this.queuedForLeaving.Add(go);
    }
}
