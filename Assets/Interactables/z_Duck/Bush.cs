using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bush : Interactable
{

    public int interactNumber, interactMaxNumber = 3;
    public UnityEvent shakeBush;
    public UnityEvent duckPopOut;
    public Item duck;

    private void Start()
    {
        duck.gameObject.SetActive(false);
    }

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract()
    {

        return interactNumber < interactMaxNumber;
    }

    // If the player is concentrating on this interactable, then pressing z will
    //  lead to interaction
    public override void Interact()
    {
        interactNumber++;

        if (interactMaxNumber == interactNumber) {
            Player.Instance.StopConcentrating(this);
            duckPopOut.Invoke();
            duck.gameObject.SetActive(true);
        } else {
            shakeBush.Invoke();
        }
    }
}
