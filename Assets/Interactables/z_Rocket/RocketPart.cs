using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPart : Item {

    public int partNum;
    private bool collected;
    private float celebrateTime = 0.5f;

    protected override bool CanInteract() {
        return !collected;
    }

    public override void Interact() {
        base.Interact();
        StartCoroutine(CelebratePartCollected());
    }

    // Makes collecting a part special
    private IEnumerator CelebratePartCollected() {
        this.collected = true;

        Player.Instance.TogglePlayerMovement(false);

        // Hold part above your head
        yield return new WaitForSeconds(celebrateTime);

        // This could be a bug if the player picks up another item before the celebrate time is up
        // If we decide to not lock player movement, we will want to add a check here for the case the
        //  player is no longer holding this rocketPart
        Player.Instance.RemoveItem(false);
        GameController.Instance.CollectedPart(this);

        Player.Instance.TogglePlayerMovement(true);
    }
}
