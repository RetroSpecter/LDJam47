using DG.Tweening;
using UnityEngine;

public class Rocket : Interactable
{
    public static string rocketReadyEvent = "rocketReady";

    private void Start()
    {
        GameController.Instance.AddEvent(rocketReadyEvent);
    }

    protected override bool CanInteract()
    {
        return true;
    }

    // If the player is concentrating on this interactable, then pressing z will
    //  lead to interaction
    public override void Interact()
    {
        if (GameController.Instance.CheckForEvent(rocketReadyEvent)) {
            Camera cam = Camera.main;
            Destroy(Player.Instance.gameObject);
            Sequence s = DOTween.Sequence();
            s.AppendInterval(3);
            s.Append(transform.GetChild(0).transform.DOShakePosition(20, 0.1f));
            s.Join(this.transform.DOMove(transform.position + transform.up * 30, 5).SetEase(Ease.InQuint));
            s.Join(cam.transform.DOMove(cam.transform.position + cam.transform.up * 20, 8).SetEase(Ease.InOutQuint));
        }
    }
}
