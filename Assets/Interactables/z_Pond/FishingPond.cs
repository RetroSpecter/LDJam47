﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPond : Interactable {

    public Item coin;
    public bool retreivedCoin;
    public Item schoolKey;
    public bool retreivedKey;
    public Item shipPiece;
    public bool retreivedPart;

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract() {
        if (!GameController.Instance.CheckForEvent("BreadGuyFed")) {
            return !retreivedCoin;
        } else if (!GameController.Instance.CheckForEvent("BreadGuyInSchool")) {
            return !retreivedKey;
        } else {
            return !retreivedPart;
        }
    }

    // If the player is concentrating on this interactable, then pressing space will
    //  lead to interaction
    public override void Interact() {
        if (!retreivedCoin) {
            retreivedCoin = true;
            // TODO: replace this with a call to fish this item onto the land
            Item.MakeItemAppear(coin);
        } else if (!retreivedKey) {
            retreivedKey = true;
            // TODO: replace this with a call to fish this item onto the land
            Item.MakeItemAppear(schoolKey);
        } else {
            retreivedPart = true;

            // TODO: replace this with a call to fish this item onto the land
            Item.MakeItemAppear(shipPiece);
        } 
    }
}
