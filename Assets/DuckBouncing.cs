using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBouncing : MonoBehaviour
{

    Vector2 initalPos;
    public float bounceRate = 4;
    public float bounceHeight = 0.1f;

    float random;

    private void Start()
    {
        initalPos = transform.position;
        random = Random.RandomRange(0.0f, 1.0f);
    }

    void Update()
    {
        float a = Mathf.Abs(Mathf.Sin(bounceRate * Time.time + random));
        transform.position = (Vector3)initalPos + transform.up * bounceHeight * a;
    }
}
