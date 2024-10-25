using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadle : MonoBehaviour
{
    [SerializeField] private float stateTimer;
    private float elapsedTime = 0;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= stateTimer)
        {
            boxCollider.enabled = false;
        }
    }
}
