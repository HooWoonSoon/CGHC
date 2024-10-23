using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEffectController : MonoBehaviour
{
    public float maxSize;
    public float groundSpeed;
    public bool canGrow;
    
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(maxSize, maxSize), groundSpeed * Time.deltaTime);
    }
}
