using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ControlValve : MonoBehaviour
{
    [SerializeField] private List<Valves> valves;
    private BoxCollider2D boxCollider;
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
        SetValveState(IsBoxDetected());
    }

    private bool IsBoxDetected()
    {
        return Physics2D.OverlapBoxAll(center, size, 0f).Any(collider => collider.GetComponent<BoxController>() != null);
    }

    private void SetValveState(bool open)
    {
        foreach (var valve in valves)
            valve.swicthOn = open;
    }
}
