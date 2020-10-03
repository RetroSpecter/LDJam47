using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    public Collider2D enterCollider;
    public Collider2D exitCollider;

    public void OnEnterRoom() {
        CameraRotationManager.instance.RotateCameraTo(this.transform);
        enterCollider.isTrigger = false;
        exitCollider.isTrigger = true;

        StartCoroutine(SwitchColliders(true));
    }

    public void OnExitRoom()
    {
        enterCollider.isTrigger = true;
        exitCollider.isTrigger = false;

        StartCoroutine(SwitchColliders(false));
    }

    //TODO: this is pretty ugly. Ther is also a corner case that can cause the player to get stuck behind a barrier
    IEnumerator SwitchColliders(bool on) {
        yield return new WaitForSeconds(0.25f);
        enterCollider.gameObject.layer = on ? 8 : 0;
        exitCollider.gameObject.layer = on ? 0 : 8;
    }
}
