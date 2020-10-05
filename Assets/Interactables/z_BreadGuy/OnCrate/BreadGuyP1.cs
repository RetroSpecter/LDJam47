using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadGuyP1 : Interactable {

    public bool gotBread;
    public GameObject breadGuyOutsideGate;
    public GameObject Teacher;

    public Collider2D rocketCollider;
    public SpriteRenderer rocketSprite;


    protected override void Start() {
        GameController.Instance.RegisterQuest(this.interactableID);
    }


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
        GameController.Instance.AddEvent("BreadGuyFed");

        var currArea = GameController.Instance.GetCurrArea();
        var nextArea = GameController.Instance.GetArea(currArea.areaNum + 1);

        nextArea.AddToEnteringQueue(() => {
            rocketCollider.enabled = true;
            rocketSprite.sortingLayerName = "Interactables";
            rocketSprite.sortingOrder = 3;
        });

        GameController.Instance.CompleteQuest(this.interactableID);
    }

}
