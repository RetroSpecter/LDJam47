using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : Interactable {

    public UnityEvent interactEvent;
    public UnityEvent itemAppearEvent;

    public override void Interact() {
        Debug.Log("You picked up an item!");

        // Add cool particles for picking up
        interactEvent.Invoke();

        // pick up the item
        Player.Instance.PickUpItem(this);
    }

    public virtual void dropItem() {

    }

    public void ItemAppear() {
        // TODO: add juice
        itemAppearEvent.Invoke();
    }

    // Given an item that is not enabled, makes it "appear" by enabling it and
    //  playing a particle system and sounds or something
    public static void MakeItemAppear(Item i) {
        i.gameObject.SetActive(true);
        i.ItemAppear();
    }
}
