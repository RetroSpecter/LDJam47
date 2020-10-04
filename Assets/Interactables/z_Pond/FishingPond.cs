using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPond : Interactable {

    public Item schoolKey;

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract() {
        return Player.IsHolding("FishingPole");
    }

    // If the player is concentrating on this interactable, then pressing space will
    //  lead to interaction
    public override void Interact() {
        if (Player.IsHolding("FishingPole")) {
            // TODO: Go fishing!
            Item.MakeItemAppear(schoolKey);
        }
    }
}
