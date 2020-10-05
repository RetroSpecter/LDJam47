using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleAndOpening : MonoBehaviour
{
    public GameObject vCam;
    public GameObject vCamTargetPosition;
    private bool inGame;
    public GameObject crashingRocket;
    public GameObject crashedRocket;
    // Start is called before the first frame update
    void Start()
    {
        crashedRocket.SetActive(false);
        Player.Instance.gameObject.SetActive(false);
        crashingRocket.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !inGame) {
            PlayOpeningSequence();
            inGame = true;
        }
    }

    void PlayOpeningSequence() {
        Sequence s = DOTween.Sequence();
        s.AppendCallback(() => {
            crashingRocket.SetActive(true);
        });
        s.Append(crashingRocket.transform.DOMoveY(vCam.transform.position.y,1)).SetEase(Ease.Linear);
        s.Append(vCam.transform.DOMoveY(vCamTargetPosition.transform.position.y, 1.5f)).SetEase(Ease.Linear);
        s.Join(crashingRocket.transform.DOMoveY(vCamTargetPosition.transform.position.y - 3, 1.5f)).SetEase(Ease.Linear);
        s.AppendCallback(() => {
            crashingRocket.SetActive(false);
            crashedRocket.SetActive(true);
        });
        s.Join(vCam.transform.DOShakePosition(1, 1));
        s.AppendCallback(() => {
            vCam.SetActive(false);
        });

        s.AppendInterval(2);

        s.AppendCallback(() => {
            Player.Instance.gameObject.SetActive(true);
            AudioManager.instance.Play("Jump");
        });
    }
}
