using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

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

            Sequence s = DOTween.Sequence();
            s.AppendCallback(() => {
                Player.Instance.TogglePlayerMovement(false);
                duck.gameObject.SetActive(true);
            });
            s.Append(duck.transform.DOMove(duck.transform.position + duck.transform.up, 0));
            s.Append(duck.transform.DOMove(duck.transform.position - duck.transform.up, 1).SetEase(Ease.OutBounce));
            s.AppendCallback(() => {
                Player.Instance.TogglePlayerMovement(true);
            });
        } else {
            shakeBush.Invoke();
            this.transform.DOShakePosition(0.5f, 0.5f, 50);
        }
    }
}
