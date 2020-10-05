using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DunkmanNPC : Interactable
{

    public bool gotDunked;
    public GameObject fallenBasketball, holdingBasketball;

    private float distFromCenter;

    private void Start()
    {
        distFromCenter = transform.position.magnitude;
        fallenBasketball.SetActive(false);
        holdingBasketball.SetActive(true);
        GameController.Instance.RegisterQuest(this.interactableID);
    }


    protected override bool CanInteract()
    {
        return !gotDunked;
    }

    public void Dunked() {
        gotDunked = true;
        StartCoroutine(dunkEnum());
    }


    //TODO: tweak this so that he always lands on the ground
    IEnumerator dunkEnum() {
        float i = 0;
        AudioManager.instance.Play("Slam dunk");
        fallenBasketball.SetActive(true);
        holdingBasketball.SetActive(false);
        fallenBasketball.transform.DOMove(fallenBasketball.transform.position - fallenBasketball.transform.up * 6, 1).SetEase(Ease.OutBounce);
        transform.DOMove(transform.position - transform.up * 0.5f, 0.2f).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(1);
        transform.DOMove(transform.position.normalized * (distFromCenter - 1), 0.5f).SetEase(Ease.InSine);
        Destroy(GetComponent<DunkmanItem>());

    }

    // If the player is concentrating on this interactable, then pressing z will
    //  lead to interaction
    public override void Interact()
    {
        //TODO: pick up item
        this.enabled = false;
    }

    // When breadman gets bread, he goes to the school gate.
    protected override void QuestResults() {
        GameController.Instance.CompleteQuest(this.interactableID);
    }
}
