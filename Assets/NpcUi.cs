using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NpcUi : MonoBehaviour
{

    public GameObject ThoughtBubble;
    public GameObject HappyStatus;

    private void Start()
    {
        HideAll();
    }

    public void HideAll() {
        ThoughtBubble.SetActive(false);
        HappyStatus.SetActive(false);
    }

    public void showDesire() {
        ThoughtBubble.SetActive(true);
    }

    public void showStatus() {
        HappyStatus.SetActive(true);
    }
}
