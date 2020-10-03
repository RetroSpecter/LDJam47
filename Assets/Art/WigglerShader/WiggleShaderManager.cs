﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WiggleShaderManager : MonoBehaviour
{

    private SpriteRenderer rend;
    private Material mat;

    public float wiggleAmp = 0.02f;
    public float wiggleScale = 5;
    public float wiggleRate = 10;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        mat = rend.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetFloat("_WiggleRate", wiggleRate);
        mat.SetFloat("_WiggleAmp", wiggleAmp);
        mat.SetFloat("_WiggleScale", wiggleScale);
    }
}
