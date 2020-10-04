using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public string interactableID;
    public NpcUi ui;

    // Start is called before the first frame update
    protected virtual void Start() {
        
    }


    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            // Register that the player is concentrating on *this* Interactable
            if (this.CanInteract())
            {
                Player.Instance.Concentrate(this);
                ui?.showDesire();
            } else {
                ui?.showStatus();
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            // Register that the player is no longer concentrating on *this* Interactable
            Player.Instance.StopConcentrating(this);
            ui?.HideAll();
        }
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



    // Easy call for any quest that takes an item. 
    protected void TakeItemFromPlayer() {
        Item i = Player.Instance.RemoveItem();
        i.transform.parent = this.transform;
        i.gameObject.SetActive(false);
    }

    protected void Sigh() {
        // TODO: Juice
    }

    // Why make a quest complete method AND a quest result method?
    // Well. The quest complete is an enumerator so we can script any kind of animation
    //  we want and time the appearance of the bread so any sounds don't overlap.
    //  this way we can be sure things happen in a good order. And so we can
    //  potentially lock the player's movement for a brief amount of time
    protected virtual IEnumerator CompleteQuest() {
        // TODO: Lock player movement for a moment?
        Player.Instance.StopConcentrating(this);
        // TODO: Yay animation
        // TODO: Quest complete sound
        yield return null;
        ui?.HideAll();
        QuestResults();
    }

    // Different things happen when NPC's 
    protected virtual void QuestResults() {
        // Nothing
    }

    /// Events
    ///////////

    // Called when an event occurrs that results in this NPC "moving" from one scene to another
    // In reality, the NPC in one scene is a different object than the NPC in the other
    // You're just enabling that different NPC and flagging this one to be disabled when
    //  the player exits the scene
    // Takes in the object in the next area you're enabling. Assumes the area in which you're disabling this object
    //  is the current area
    public void MoveAreasEvent(GameObject objectInDifferentArea) {
        this.MoveAreasEvent(new GameObject[] { objectInDifferentArea }, GameController.Instance.GetCurrArea());
    }

    
    public void MoveAreasEvent(GameObject[] objectsInDifferentArea) {
        this.MoveAreasEvent(objectsInDifferentArea, GameController.Instance.GetCurrArea());
    }

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
            Debug.Log("Setting new object to active");
            go.SetActive(true);
        }

        Debug.Log("Setting object to inactive in area " + currArea.areaNum);
        // Queue this boy to be disabled from this area
        currArea.AddToLeavingQueue(this.gameObject);
    }
}
