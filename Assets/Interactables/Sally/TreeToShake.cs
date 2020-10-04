using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeToShake : Interactable
{
    public int interactNumber, interactMaxNumber = 3;
    public UnityEvent shakeTree;
    public UnityEvent duckPopOut;
    public Item Cat, Part;
    public GameObject catInTree, partInTree;

    private void Start()
    {
        Cat.gameObject.SetActive(false);
        Part.gameObject.SetActive(false);
    }

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract()
    {
        return interactNumber < interactMaxNumber;
    }

    // If the player is concentrating on this interactable, then pressing z will
    //  lead to interaction
    public override void Interact() {
        interactNumber++;

        if (interactMaxNumber == interactNumber) {
            Player.Instance.StopConcentrating(this);
            duckPopOut.Invoke();
            Cat.gameObject.SetActive(true);
            Part.gameObject.SetActive(true);
            catInTree.SetActive(false);
            partInTree.SetActive(false);
        } else {
            shakeTree.Invoke();
        }
    }
}
