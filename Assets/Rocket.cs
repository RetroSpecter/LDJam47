using DG.Tweening;
using UnityEngine;

public class Rocket : Interactable
{
    public static string rocketReadyEvent = "rocketReady";

    public Transform[] pieces;
    private int collectedPieces;
    public GameObject vCam;

    public GameObject outline;


    private void Start() {
        //GameController.Instance.AddEvent(rocketReadyEvent);
    }


    // If the player is concentrating on this interactable, then pressing z will
    //  lead to interaction
    public override void Interact()
    {
        if (collectedPieces == pieces.Length) {

            Destroy(Player.Instance.gameObject);
            Sequence s = DOTween.Sequence();
            s.AppendInterval(1);
            s.Append(transform.GetChild(0).transform.DOShakePosition(20, 0.1f));
            s.Join(this.transform.DOMove(transform.position + transform.up * 30, 5).SetEase(Ease.InQuint));
            s.Join(vCam.transform.DOMove(vCam.transform.position + vCam.transform.up * 20, 8).SetEase(Ease.InOutQuint));
            outline.SetActive(false);
        }
    }

    // Given a part, displays it above the rocket
    public void CollectPart(RocketPart part) {
        var bleg = pieces[part.partNum];
        bleg.gameObject.SetActive(true);
        collectedPieces++;
        part.transform.position = bleg.position;
        part.transform.parent = bleg;
        part.gameObject.SetActive(false);
    }
}
