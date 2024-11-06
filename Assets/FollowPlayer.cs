using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Player player;
    [SerializeField] private float speed = 5;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Destroy(this.gameObject);
        }
    }
}
