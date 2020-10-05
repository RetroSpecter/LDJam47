using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashMan : Interactable
{
    private int numberOfChildren;
    public GameObject[] trashBags;
    public UnityEvent dumpersterOpenEvent;


    public Item part;

    private void Start()
    {
        foreach (GameObject bag in trashBags)
        {
            bag.SetActive(false);
        }
        part.gameObject.SetActive(false);
        this.gameObject.SetActive(false);

        GameController.Instance.RegisterQuest(this.interactableID);
    }

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract()
    {
        return numberOfChildren < trashBags.Length * 2;
    }

    // If the player is concentrating on this interactable, then pressing z will
    //  lead to interaction
    public override void Interact()
    {
        if (Player.IsHolding("Trash"))
        {
            TakeItemFromPlayer();
            trashBags[numberOfChildren/2].SetActive(true);

            Player.Instance.StopConcentrating(this);
            numberOfChildren++;

            if (numberOfChildren == trashBags.Length * 2)
            {
                StartCoroutine(this.CompleteQuest());
                foreach (GameObject bag in trashBags)
                {
                    bag.SetActive(false);
                }

                dumpersterOpenEvent.Invoke();
                part.gameObject.SetActive(true);
            }

        }
    }

    // When breadman gets bread, he goes to the school gate.
    protected override void QuestResults() {
        GameController.Instance.CompleteQuest(this.interactableID);
    }
}
