using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlGate : MonoBehaviour
{
    [SerializeField] private AutoGate autoGate;
    private BoxCollider2D boxCollider;
    private bool isOpen = false;
    private Vector2 center;
    private Vector2 size;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        center = boxCollider.bounds.center;
        size = boxCollider.bounds.size;
    }

    private void Update()
    {
        IsBoxDetected();
        if (IsBoxDetected() && isOpen == false)
        {
            autoGate.CloseOpenGate(true);
            isOpen = true;
        }
        else if (!IsBoxDetected() && isOpen == true)
        {
            autoGate.CloseOpenGate(false);
            isOpen = false;
        }
    }

    private bool IsBoxDetected()
    {
        return Physics2D.OverlapBoxAll(center, size, 0f).Any(collider => collider.GetComponent<BoxController>() != null);
    }
}