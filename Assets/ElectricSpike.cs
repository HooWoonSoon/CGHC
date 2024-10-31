using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSpike : MonoBehaviour
{
    [SerializeField] private SpriteRenderer chain;
    [SerializeField] private GameObject movableSpike;
    [SerializeField] private float stateTimer;
    private Vector2 endPositionAbove;
    private Vector2 endPositionBelow;
    private float timer;

    void Start()
    {
        Bounds bounds = chain.bounds;
        endPositionAbove = new Vector2(bounds.center.x, bounds.max.y);
        endPositionBelow = new Vector2(bounds.center.x, bounds.min.y);
        
    }

    void Update()
    {
        IsBoxDetected();
        if (IsBoxDetected())
        {
            timer += Time.deltaTime;
            movableSpike.transform.position = Vector2.Lerp(endPositionAbove, endPositionBelow, timer);
        }
    }

    private bool IsBoxDetected()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(movableSpike.transform.position, movableSpike.transform.localScale, 0);
        foreach (Collider2D collider in colliders)
        {
            BoxController boxController = collider.GetComponent<BoxController>();
            if (boxController != null)
            {
                boxController.transform.SetParent(movableSpike.transform);
                if (boxController.GroundDetected() == false)
                {
                    boxController.transform.SetParent(null);
                }
                return true;    
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(movableSpike.transform.position, movableSpike.transform.localScale);
    }

}
