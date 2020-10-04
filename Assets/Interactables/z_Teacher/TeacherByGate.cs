using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherByGate : Interactable {

    public Item fishingPole;
    public bool fishingPoleGiven;
    public bool gateUnlocked;

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract() {
        return GameController.Instance.CheckForEvent("BreadGuyFed") && !gateUnlocked;
    }

    // If the player is concentrating on this interactable, then pressing space will
    //  lead to interaction
    public override void Interact() {
        if (!fishingPoleGiven) {
            Item.MakeItemAppear(fishingPole);
            Player.Instance.StopConcentrating(this);
            this.fishingPoleGiven = true;
        } else if (!Player.IsHolding("GateKey")) {
            Debug.Log("I need help, Jose");
            Sigh();
        } else {
            TakeItemFromPlayer();
            gateUnlocked = true;
            StartCoroutine(this.CompleteQuest());
        }
    }

    // When baker gets wheat, he makes bread.
    protected override void QuestResults() {

        // What happens?
    }
}
