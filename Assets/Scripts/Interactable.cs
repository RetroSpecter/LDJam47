using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public string interactableID;

    // Start is called before the first frame update
    protected virtual void Start() {
        
    }


    protected void OnTriggerEnter2D(Collider2D collision) {
        // Register that the player is concentrating on *this* Interactable
        if (this.CanInteract()) {
            Player.Instance.Concentrate(this);
        }

    }

    protected void OnTriggerExit2D(Collider2D collision) {
        // Register that the player is no longer concentrating on *this* Interactable
        Player.Instance.StopConcentrating(this);
    }


    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected virtual bool CanInteract() {
        return true;
    }

    // If the player is concentrating on this interactable, then pressing space will
    //  lead to interaction
    public virtual void Interact() {
        Debug.Log("You interacted with me!");
    }

    /// Events
    ///////////

    // Called when an event occurrs that results in this NPC "moving" from one scene to another
    // In reality, the NPC in one scene is a different object than the NPC in the other
    // You're just enabling that different NPC and flagging this one to be disabled when
    //  the player exits the scene
    public void MoveAreasEvent(GameObject objectInDifferentArea, Area currArea) {
        this.MoveAreasEvent(new GameObject[] { objectInDifferentArea }, currArea);
    }

    // Called when an event occurrs that results in this NPC "moving" from one scene to another
    // In reality, the NPC in one scene is a different object than the NPC in the other
    // You're just enabling that different NPC and flagging this one to be disabled when
    //  the player exits the scene
    public void MoveAreasEvent(GameObject[] objectsInDifferentArea, Area currArea) {
        // enable the given objects in the other area
        foreach (GameObject go in objectsInDifferentArea) {
            go.SetActive(true);
        }

        // Queue this boy to be disabled from this area
        currArea.AddToLeavingQueue(this.gameObject);
    }
}
