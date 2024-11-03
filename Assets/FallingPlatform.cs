using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector2 defaultPos;
    [SerializeField] float fallDelay, respawnTime;

    void Start()
    {
        defaultPos = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = PlayerManager.instance.player;
        if (player != null) 
        {
            StartCoroutine("PlatformDrop");
        }
    }

    IEnumerator PlatformDrop()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(respawnTime);
    }
}
