using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxTestArea1 : Interactable {

    public GameObject Area2FoxGuy;

    // If the player is concentrating on this interactable, then pressing space will
    //  lead to interaction
    public override void Interact() {
        Debug.Log("You interacted with me! Time for me to poof to the next area");
        this.MoveAreasEvent(Area2FoxGuy, GameController.Instance.GetCurrArea());
    }
}
