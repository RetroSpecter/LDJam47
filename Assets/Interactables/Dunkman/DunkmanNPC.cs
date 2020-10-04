using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DunkmanNPC : Interactable
{

    public bool gotDunked;
    public GameObject basketball;

    private float distFromCenter;

    private void Start()
    {
        distFromCenter = transform.position.magnitude;
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
        basketball.transform.DOMove(basketball.transform.position - basketball.transform.up * 4, 1).SetEase(Ease.OutBounce);
        transform.DOMove(transform.position - transform.up * 0.5f, 0.2f).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(1);
        transform.DOMove(transform.position.normalized * (distFromCenter - 1), 0.5f).SetEase(Ease.InSine);
        GetComponent<DunkmanItem>().enabled = false;

    }

    // If the player is concentrating on this interactable, then pressing z will
    //  lead to interaction
    public override void Interact()
    {
        //TODO: pick up item
        this.enabled = false;
    }
}
