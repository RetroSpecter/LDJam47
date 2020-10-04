using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{

    public SpriteRenderer rend;

    /*
    private void Update()
    {
        SpriteWalk(1);
    }
    */


    public void SpriteWalk(float velocity) {
        rend.flipX = velocity < 0;

        float amp = 0;
       if (Mathf.Abs(velocity) > 0.1f)
        {
           amp = Mathf.Sin(Time.time * 10);
        }

        Vector3 s = transform.localScale;
        s.y = Mathf.Lerp(1, 0.85f, Mathf.Abs(amp));
        s.x = Mathf.Lerp(1, 1.1f, Mathf.Abs(amp));
        transform.localScale = s;

        
        Vector3 r = transform.localEulerAngles ;
        r.z = Mathf.Lerp(0, -10f, Mathf.Abs(amp));
        transform.localEulerAngles = r;
        
    }
}
