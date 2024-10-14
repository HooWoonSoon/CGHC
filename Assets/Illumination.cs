using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illumination : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] Material changeMt;
    private Material originalMt;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalMt = sr.material;
    }

    public void SlideLight(bool isllumitation)
    {
        if (isllumitation)
        {
            sr.material = changeMt;
        }
        else
            sr.material = originalMt;
    }
}
