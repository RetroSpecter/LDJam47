using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishingPond : Interactable {

    public Item coin;
    public bool retreivedCoin;
    public Item schoolKey;
    public bool retreivedKey;
    public Item shipPiece;
    public bool retreivedPart;

    [Space()]
    public GameObject fishingRod;

    private void Start()
    {
        fishingRod.SetActive(false);
        coin.gameObject.SetActive(false);
        schoolKey.gameObject.SetActive(false);
        shipPiece.gameObject.SetActive(false);
    }

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract() {
        return !retreivedCoin || !retreivedKey || !retreivedPart;
    }

    // If the player is concentrating on this interactable, then pressing space will
    //  lead to interaction
    public override void Interact() {
        
        if (!retreivedCoin) {
            retreivedCoin = true;
            // TODO: replace this with a call to fish this item onto the land
            //Item.MakeItemAppear(coin);
            Sequence s = DOTween.Sequence();
            s.AppendCallback(() => {
                Player.Instance.TogglePlayerMovement(false);
                fishingRod.SetActive(true);
            });

            s.AppendInterval(1);

            s.Append(fishingRod.transform.DOMove(fishingRod.transform.position - fishingRod.transform.up * 0.5f, 0.25f));


            s.AppendInterval(3);

            s.AppendCallback(() => {
                Item.MakeItemAppear(coin);
                coin.transform.parent = fishingRod.transform;
                coin.transform.position += coin.transform.up * 1f;
            });
            s.Append(fishingRod.transform.DOMove(fishingRod.transform.position + fishingRod.transform.up * 2f, 0.25f));

            s.AppendInterval(1);

            s.AppendCallback(() => {
                coin.transform.parent = null;
                coin.transform.position = transform.position - transform.right * 3;
                fishingRod.SetActive(false);
                Player.Instance.TogglePlayerMovement(true);
                fishingRod.SetActive(false);
                                fishingRod.transform.position -= fishingRod.transform.up * 1.5f;
            });

        } else
        if (!retreivedKey) {
            retreivedKey = true;
            // TODO: replace this with a call to fish this item onto the land
            //
            Sequence s = DOTween.Sequence();
            s.AppendCallback(() => {
                Player.Instance.TogglePlayerMovement(false);
                fishingRod.SetActive(true);
            });

            s.AppendInterval(1);

            s.Append(fishingRod.transform.DOMove(fishingRod.transform.position - fishingRod.transform.up * 0.5f, 0.25f));


            s.AppendInterval(3);
            s.Append(fishingRod.transform.DOShakePosition(2, 0.1f, 50));
            s.AppendCallback(() => {
                Item.MakeItemAppear(schoolKey);
                schoolKey.transform.parent = fishingRod.transform;
                //schoolKey.transform.position += schoolKey.transform.up * 1f;
            });

            s.Append(fishingRod.transform.DOMove(fishingRod.transform.position + fishingRod.transform.up * 2f, 0.25f));
 
            s.AppendInterval(1);


            s.AppendCallback(() => {
                schoolKey.transform.parent = null;
                schoolKey.transform.position = transform.position - transform.right * 3;
                fishingRod.SetActive(false);
                Player.Instance.TogglePlayerMovement(true);
                fishingRod.SetActive(false);
                                fishingRod.transform.position -= fishingRod.transform.up * 1.5f;

            });
        } else {
        retreivedPart = true;

            // TODO: replace this with a call to fish this item onto the land
            //Item.MakeItemAppear(shipPiece);

            Sequence s = DOTween.Sequence();
            s.AppendCallback(() => {
                Player.Instance.TogglePlayerMovement(false);
                fishingRod.SetActive(true);
            });

            s.AppendInterval(1);

            s.Append(fishingRod.transform.DOMove(fishingRod.transform.position - fishingRod.transform.up * 0.5f, 0.25f));


            s.AppendInterval(3);
            s.Append(fishingRod.transform.DOShakePosition(2, 0.1f, 50));
            s.Append(fishingRod.transform.DOShakePosition(2, 0.1f, 50));
            s.Join(transform.DOShakePosition(2, 0.1f, 50));
            s.AppendCallback(() => {
                Item.MakeItemAppear(shipPiece);
                shipPiece.transform.parent = fishingRod.transform;
                //shipPiece.transform.position += schoolKey.transform.up * 1f;
            });

            s.Append(fishingRod.transform.DOMove(fishingRod.transform.position + fishingRod.transform.up * 2f, 0.25f));

            s.AppendInterval(1);


            s.AppendCallback(() => {
                shipPiece.transform.parent = null;
                shipPiece.transform.position = transform.position - transform.right * 3;
                fishingRod.SetActive(false);
                Player.Instance.TogglePlayerMovement(true);
                fishingRod.SetActive(false);
                fishingRod.transform.position -= fishingRod.transform.up * 1.5f;
            });
        } 
    }
}
