using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 defaultPos;
    [SerializeField] float fallDelay, respawnTime;
    private float stateTimer;

    void Start()
    {
        defaultPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if (stateTimer >= respawnTime)
        {
            StopAllCoroutines();
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.position = defaultPos;
            stateTimer = 0;
        }
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
        rb.bodyType = RigidbodyType2D.Dynamic;
        stateTimer += Time.deltaTime;
    }
}
