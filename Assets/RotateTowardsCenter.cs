using UnityEngine;

[ExecuteInEditMode]
public class RotateTowardsCenter : MonoBehaviour
{


    void Update()
    {
        transform.up = new Vector2(transform.position.x, transform.position.y);
    }
}
