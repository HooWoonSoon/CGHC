using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eletricalSpike : MonoBehaviour
{
    private Vector2 originPosition;
    private BoxCollider2D boxCollider2d;

    #region internal
    private Bounds bounds;
    private Vector2 boundLeft;
    private Vector2 boundRight;
    private Vector2 boundCentralAbove;
    private Vector2 boundCentralBottom;
    private Vector2 size;
    #endregion
    private void Awake()
    {
        boxCollider2d = GetComponentInChildren<BoxCollider2D>();
    }
    void Start()
    {
        originPosition = this.transform.position;
    }

    private void Update()
    {
        BoxDetected();
    }
    private bool BoxDetected()
    {
        bounds = boxCollider2d.bounds;
        boundLeft = new Vector2(bounds.min.x, bounds.center.y);
        boundRight = new Vector2(bounds.max.x, bounds.center.y);
        boundCentralAbove = new Vector2(bounds.center.x, bounds.max.y);
        boundCentralBottom = new Vector2(bounds.center.x, bounds.min.y);

        float weight = Vector2.Distance(boundLeft, boundRight);

        size = new Vector2(weight, 1f);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(boundCentralAbove, size, 0);
        foreach (var hit in colliders)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(boundCentralAbove, size);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boundCentralBottom, size);
    }
}
