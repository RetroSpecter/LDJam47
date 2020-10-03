using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable {

    protected override bool CanInteract() {
        return true;
    }

    public override void Interact() {
        Debug.Log("You picked up an item!");

        // Add cool particles for picking up

        // pick up the item
        Player.Instance.PickUpItem(this);
    }


    // Given an item that is not enabled, makes it "appear" by enabling it and
    //  playing a particle system and sounds or something
    public static void MakeItemAppear(Item i) {
        i.gameObject.SetActive(true);

        // TODO: add juice

        // TODO: play quest completed sound
    }
}
