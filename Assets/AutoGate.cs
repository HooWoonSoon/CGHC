using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGate : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D boxCollider2D;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = true;
    }
    public void OpenGate()
    {
        //anim.SetBool("Open", true);
        boxCollider2D.enabled = false;
        Debug.Log(false);

    }

    public void CloseGate()
    {
        //anim.SetBool("Close", true);
        boxCollider2D.enabled = true;
        Debug.Log(true);
    }
}
