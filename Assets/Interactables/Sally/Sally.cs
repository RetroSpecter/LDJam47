using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sally : Interactable
{
    private bool gotCat;
    public GameObject catAppear;

    private void Start()
    {
        catAppear.SetActive(false);
        GameController.Instance.RegisterQuest(this.interactableID);
    }

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract()
    {
        return !gotCat;
    }


    // If the player is concentrating on this interactable, then pressing z will
    //  lead to interaction
    public override void Interact()
    {
        if (Player.IsHolding("Cat")) {
            TakeItemFromPlayer();
            gotCat = true;
            catAppear.SetActive(true);
            StartCoroutine(this.CompleteQuest());
        } else {
            Sigh();
        }
    }

    // When breadman gets bread, he goes to the school gate.
    protected override void QuestResults() {
        GameController.Instance.CompleteQuest(this.interactableID);
    }

}
