using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherByGate : Interactable {

    //public Item fishingPole;
    //public bool fishingPoleGiven;
    public bool gateUnlocked;
    public GameObject breadGuyByGate;
    public GameObject trashman;
    public GameObject graduatedBreadGuy;

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract() {
        return GameController.Instance.CheckForEvent("BreadGuyFed") && !gateUnlocked;
    }

    // If the player is concentrating on this interactable, then pressing space will
    //  lead to interaction
    public override void Interact() {
        if (!Player.IsHolding("GateKey")) {
            Sigh();
        } else {
            TakeItemFromPlayer();
            gateUnlocked = true;
            StartCoroutine(this.CompleteQuest());
        }
    }

    // When baker gets wheat, he makes bread.
    protected override void QuestResults() {
        // Teacher and BRead guy are gone to school
        GameController.Instance.GetCurrArea().AddToLeavingQueue(this.gameObject);
        GameController.Instance.GetCurrArea().AddToLeavingQueue(this.breadGuyByGate);
        // Trash man appears
        trashman.SetActive(true);

        GameController.Instance.AddEvent("BreadGuyInSchool");

        var crashSite = GameController.Instance.GetArea(1);

        // Make it so that when the player enters area 1, they need to make
        //  a full loop (and enter area 1 again) before graduated bread guy will
        //  appear
        crashSite.AddToEnteringQueue(() => {
            crashSite.AddToEnteringQueue(() => {
                this.gameObject.SetActive(true);
                graduatedBreadGuy.SetActive(true);
                GameController.Instance.AddEvent("BreadGuyGraduated");
            });
        });
    }
}
