using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BestEndingSequence : Interactable
{
    public GameObject vCam;
    public GameObject[] friends;
    public TextMeshPro text;

    // Based on the state of the game & the player's inventory, checks if the
    //  player can interact with this object. Returns if it can or not.
    protected override bool CanInteract()
    {
        return true;
    }

    public override void Interact()
    {
        text.text = "You and your friends escaped a Loop";
        Player.Instance.gameObject.SetActive(false);

        Sequence s = DOTween.Sequence();

        foreach (GameObject fren in friends) {
            s.AppendCallback(() => {
                Sequence s1 = DOTween.Sequence();
                s1.Append(fren.transform.DOMove(Player.Instance.transform.position,3));
                s1.AppendCallback(() => { fren.gameObject.SetActive(false);  });
            });
        }

        s.AppendInterval(5);
        s.Append(transform.GetChild(0).transform.DOShakePosition(20, 0.1f));
        s.Join(this.transform.DOMove(transform.position + transform.up * 30, 5).SetEase(Ease.InQuint));
        s.Join(vCam.transform.DOMove(vCam.transform.position + vCam.transform.up * 20, 8).SetEase(Ease.InOutQuint));
    }
    
}
