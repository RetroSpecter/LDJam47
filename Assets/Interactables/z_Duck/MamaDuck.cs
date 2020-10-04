using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaDuck : Interactable
{
    private int numberOfChildren;
    public GameObject[] ducks;

    private void Start()
    {
        foreach (GameObject duck in ducks) {
            duck.SetActive(false);
        }
    }

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract()
    {
        return numberOfChildren < ducks.Length;
    }

    // If the player is concentrating on this interactable, then pressing z will
    //  lead to interaction
    public override void Interact()
    {
        if (Player.IsHolding("Duck1")) {
            TakeItemFromPlayer();
            ducks[0].SetActive(true);

            Player.Instance.StopConcentrating(this);
            numberOfChildren++;

            if (numberOfChildren == ducks.Length)
                StartCoroutine(this.CompleteQuest());

        }

        if (Player.IsHolding("Duck2")) {
            TakeItemFromPlayer();
            ducks[1].SetActive(true);

            Player.Instance.StopConcentrating(this);
            numberOfChildren++;

            if (numberOfChildren == ducks.Length)
                StartCoroutine(this.CompleteQuest());
        }

        if (Player.IsHolding("Duck3")) {
            TakeItemFromPlayer();
            ducks[2].SetActive(true);

            Player.Instance.StopConcentrating(this);
            numberOfChildren++;

            if (numberOfChildren == ducks.Length)
                StartCoroutine(this.CompleteQuest());
        }
    }

    protected  override IEnumerator CompleteQuest()
    {
        // TODO: Lock player movement for a moment?
        Player.Instance.StopConcentrating(this);
        // TODO: Yay animation
        print("helped the ducks");
        // TODO: Quest complete sound
        yield return null;

        QuestResults();
    }

    // When breadman gets bread, he goes to the school gate.
    protected override void QuestResults()
    {
        //MoveAreasEvent(this.breadGuyOutsideGate);
    }
}
