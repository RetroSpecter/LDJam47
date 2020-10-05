using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunkmanItem : Item
{
    private DunkmanNPC npc;

    private void Start()
    {
        npc = GetComponent<DunkmanNPC>();
    }

    public override void Interact()
    {

        // Add cool particles for picking up
        interactEvent.Invoke();

        // pick up the item
        Player.Instance.PickUpItem(this);
    }

    public override void dropItem() {
        npc.enabled = true;
    }

}
