using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSpike : MonoBehaviour
{
    [SerializeField] private SpriteRenderer chain;
    [SerializeField] private GameObject movableSpike;
    [SerializeField] private float stateTimer = 1f;
    private GameManager gameManager;
    private Vector2 endPositionAbove;
    private Vector2 endPositionBelow;
    private Vector2 currentPosition;
    private float timer;

    void Start()
    {
        Bounds bounds = chain.bounds;
        endPositionAbove = new Vector2(bounds.center.x, bounds.max.y);
        endPositionBelow = new Vector2(bounds.center.x, bounds.min.y);
        gameManager = GameManager.instance;
    }

    void Update()
    {
        Detected();
        currentPosition = movableSpike.transform.position;
        if (Detected())
        {
            timer += Time.deltaTime;
            movableSpike.transform.position = Vector2.Lerp(endPositionAbove, endPositionBelow, timer/stateTimer);
            if (timer >= stateTimer)
            {
                timer = stateTimer;
            }
        }
        else if (!Detected() && currentPosition != endPositionAbove)
        {
            timer -= Time.deltaTime;
            movableSpike.transform.position = Vector2.Lerp(endPositionAbove, endPositionBelow, timer/stateTimer);
            if (timer <= 0f)
            {
                timer = 0f;
            }
        }
    }

    private bool Detected()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(movableSpike.transform.position, movableSpike.transform.localScale * 1.1f, 0);
        foreach (Collider2D collider in colliders)
        {
            BoxController boxController = collider.GetComponent<BoxController>();
            PlayerDeathHandler deathHandler = collider.GetComponent<PlayerDeathHandler>();
            if (boxController != null)
            {
                if (boxController.VasicouisDetected())
                {
                    boxController.transform.SetParent(movableSpike.transform);
                }
                else if (boxController.VasicouisDetected() == false)
                {
                    boxController.transform.SetParent(null);
                }
                return true;    
            }
            if (deathHandler != null)
            {
                deathHandler.TriggerDeath();
                gameManager.UpdateDead();
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
