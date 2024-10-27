using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadle : MonoBehaviour
{
    [SerializeField] private float activeTime = 3f;    
    [SerializeField] private float inactiveTime = 2f;  
    [SerializeField] private bool startActive = true; 

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        SetPlatformState(startActive);
        StartCoroutine(PlatformCycle());
    }

    private IEnumerator PlatformCycle()
    {
        while (true)
        {
            if (startActive)
            {
                yield return new WaitForSeconds(activeTime);
                SetPlatformState(false);
                yield return new WaitForSeconds(inactiveTime);
                SetPlatformState(true);
            }
            else
            {
                yield return new WaitForSeconds(inactiveTime);
                SetPlatformState(true);
                yield return new WaitForSeconds(activeTime);
                SetPlatformState(false);
            }
        }
    }

    private void SetPlatformState(bool active)
    {
        boxCollider.enabled = active;
        anim.SetBool("Activated", active);
    }
}
