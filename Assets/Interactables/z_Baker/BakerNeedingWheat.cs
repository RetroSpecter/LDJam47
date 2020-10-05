using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakerNeedingWheat : Interactable {

    private bool breadMade;
    public Item bread;

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract() {
        return !breadMade;
    }

    public override void Interact() {
        if (!Player.IsHolding("Coin")) {
            Sigh();
        } else {
            TakeItemFromPlayer();
            breadMade = true;
            StartCoroutine(this.CompleteQuest());
            
        }
    }

    // When baker gets wheat, he makes bread.
    protected override void QuestResults() {
        Item.MakeItemAppear(bread);
    }
}
