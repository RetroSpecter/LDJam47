using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadGuyP1 : Interactable {

    public bool gotBread;
    public GameObject breadGuyOutsideGate;
    public GameObject Teacher;

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract() {
        return !gotBread;
    }

    // If the player is concentrating on this interactable, then pressing z will
    //  lead to interaction
    public override void Interact() {
        if (!Player.IsHolding("Bread")) {
            // TODO: sigh
        } else {
            TakeItemFromPlayer();
            gotBread = true;
            StartCoroutine(this.CompleteQuest());
        }
    }

    // When breadman gets bread, he goes to the school gate.
    protected override void QuestResults() {
        MoveAreasEvent(new GameObject[] { this.breadGuyOutsideGate });

    }

}
