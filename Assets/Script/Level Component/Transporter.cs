using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Transporter : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private float originalMoveSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
            originalMoveSpeed = player.moveSpeed;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (player.facingRight == true)
            {
                player.moveSpeed = originalMoveSpeed * speed;
            }
            else
            {
                player.moveSpeed = originalMoveSpeed / speed;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
            player.moveSpeed = originalMoveSpeed;
    }

}
