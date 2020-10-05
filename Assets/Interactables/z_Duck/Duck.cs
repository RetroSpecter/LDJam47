using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : Item {

    public override void Interact() {
        Debug.Log("You picked up an item!");

        // Add cool particles for picking up
        interactEvent.Invoke();

        // pick up the item
        Player.Instance.PickUpItem(this, false);
        AudioManager.instance.Play("Quack");
    }
}
