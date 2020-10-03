using UnityEngine;
using DG.Tweening;

public class CameraRotationManager : MonoBehaviour
{
    public static CameraRotationManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void RotateCameraTo(Transform target) {
        transform.DOLocalRotate(target.eulerAngles, 1, RotateMode.Fast);
    }
}
