using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable {



    public override void Interact() {
        Debug.Log("You picked up an item!");

        // Add cool particles for picking up

        // pick up the item
        Player.Instance.PickUpItem(this);
    }

}
