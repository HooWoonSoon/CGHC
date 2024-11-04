using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 defaultPos;
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float respawnTime = 2f;
    private Coroutine dropCoroutine;

    void Start()
    {
        defaultPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = PlayerManager.instance.player;

        if (player != null)
        {
            if (dropCoroutine != null)
            {
                StopCoroutine(dropCoroutine);
            }
            dropCoroutine = StartCoroutine(PlatformDrop());
        }
    }

    private IEnumerator PlatformDrop()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;  
        yield return new WaitForSeconds(respawnTime);
        ResetPlatform();
    }

    private void ResetPlatform()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;  
        transform.position = defaultPos;  
        dropCoroutine = null; 
    }
}
