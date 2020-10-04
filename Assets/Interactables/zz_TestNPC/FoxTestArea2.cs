using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxTestArea2 : Interactable {

    public Transform headPosition;
    private bool holdingItem;

    protected override bool CanInteract() {
        return Player.Instance.GetHeldItemID() == "TestItem" && !holdingItem;
    }

    // If the player is concentrating on this interactable, then pressing space will
    //  lead to interaction
    public override void Interact() {
        Debug.Log("You're holding an item. Let me take that off your hands");
        var item = Player.Instance.RemoveItem();
        item.GetComponent<Collider2D>().enabled = false;
        item.transform.position = this.headPosition.position;
        item.transform.SetParent(this.headPosition);
    }
}
