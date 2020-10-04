using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class TreeToShake : Interactable
{
    public int interactNumber, interactMaxNumber = 5;
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

        if (interactMaxNumber-2 == interactNumber) {
            Sequence partsFall = DOTween.Sequence();
            partsFall.AppendCallback(() => {
                Player.Instance.TogglePlayerMovement(false);
            });
            partsFall.Append(partInTree.transform.DOMove(Part.transform.position, 1.3f).SetEase(Ease.OutBounce));
            partsFall.Join(partInTree.transform.DORotate(Part.transform.eulerAngles, 1.3f));
            partsFall.AppendCallback(() => {
                Part.gameObject.SetActive(true);
                partInTree.SetActive(false);
                Player.Instance.TogglePlayerMovement(true);
            });
        }
        if (interactMaxNumber == interactNumber) {
            Player.Instance.StopConcentrating(this);
            duckPopOut.Invoke();
            
            this.transform.GetChild(0).DOShakePosition(0.5f, 0.5f, 50);

            Sequence CatFall = DOTween.Sequence();
            CatFall.AppendCallback(() => {
                Player.Instance.TogglePlayerMovement(false);
            });
            CatFall.Append(catInTree.transform.DOMove(Cat.transform.position, 1.25f).SetEase(Ease.OutBounce));
            CatFall.Join(catInTree.transform.DORotate(Cat.transform.eulerAngles, 1.3f));
            CatFall.AppendCallback(() => {
                Cat.gameObject.SetActive(true);
                catInTree.SetActive(false);
                Player.Instance.TogglePlayerMovement(true);
            });
        } else {
            shakeTree.Invoke();
            this.transform.GetChild(0).DOShakePosition(0.5f, 0.5f, 50);
        }
    }
}
