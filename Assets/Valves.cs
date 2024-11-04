using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Valves : MonoBehaviour
{
    [SerializeField] private Transform nonOpenEndPosion;
    [SerializeField] private Transform OpenEndPosition;
    [SerializeField] private float stateTimer = 1f;
    private BoxCollider2D boxCollider;
    private float timer;

    public bool swicthOn;
    
    #region internal
    private float offset;
    private float AboveY;
    private float BelowY;
    #endregion

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Bounds bounds = boxCollider.bounds;
        Vector2 aboveCenterBounds = new Vector2(bounds.center.x, bounds.max.y);
        Vector2 centerBounds = new Vector2(bounds.center.x, bounds.center.y);
        offset = Vector2.Distance(aboveCenterBounds, centerBounds);
        AboveY = nonOpenEndPosion.position.y - offset;
        BelowY = OpenEndPosition.position.y + offset;
    }
    
    private void Update()
    {
        if (swicthOn == true)
        {
            timer += Time.deltaTime;
            this.transform.position = Vector2.Lerp(new Vector2(nonOpenEndPosion.position.x, AboveY),
                new Vector2(OpenEndPosition.position.x, BelowY), timer / stateTimer);
            if (timer >= stateTimer)
            {
                timer = stateTimer;
            }
        } 
        else
        {
            timer -= Time.deltaTime;
            this.transform.position = Vector2.Lerp(new Vector2(nonOpenEndPosion.position.x, AboveY),
                new Vector2(OpenEndPosition.position.x, BelowY), timer / stateTimer);
            if (timer <= 0f)
            {
                timer = 0f;
            }
        }

    }
}
