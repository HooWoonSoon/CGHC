using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToWayPlatform : MonoBehaviour
{
    private GameObject currentPlatformCollider;
    private CapsuleCollider2D playerCollider;

    private void Start()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentPlatformCollider != null)
            {
                StartCoroutine(DisablePlatformCoroutine());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "OneWayPlatform")
            currentPlatformCollider = other.gameObject;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "OneWayPlatform")
            currentPlatformCollider = null;
    }

    private IEnumerator DisablePlatformCoroutine()
    {
        BoxCollider2D platformCollider = currentPlatformCollider.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
