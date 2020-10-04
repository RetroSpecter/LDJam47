using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Interactable {

    public bool tilled;
    public Item wheat;

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract() {
        return !tilled;
    }

    // When player interacts with farm, they get wheat.
    public override void Interact() {
        tilled = true;
        Player.Instance.StopConcentrating(this);
        Item.MakeItemAppear(wheat);
    }
}
