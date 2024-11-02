using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSpike : MonoBehaviour
{
    [SerializeField] private SpriteRenderer chain;
    [SerializeField] private GameObject movableSpike;
    [SerializeField] private float stateTimer = 1f;
    private Vector2 endPositionAbove;
    private Vector2 endPositionBelow;
    private Vector2 currentPosition;
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
        currentPosition = movableSpike.transform.position;
        if (IsBoxDetected())
        {
            timer += Time.deltaTime;
            movableSpike.transform.position = Vector2.Lerp(endPositionAbove, endPositionBelow, timer/stateTimer);
            if (timer >= stateTimer)
            {
                timer = stateTimer;
            }
        }
        else if (!IsBoxDetected() && currentPosition != endPositionAbove)
        {
            timer -= Time.deltaTime;
            movableSpike.transform.position = Vector2.Lerp(endPositionAbove, endPositionBelow, timer/stateTimer);
            if (timer <= 0f)
            {
                timer = 0f;
            }
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
                if (boxController.VasicouisDetected() == false)
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
