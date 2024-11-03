using UnityEngine;

public class DoubleJumpObject : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>(); // Assumes there's only one player
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.SetMaxJumps(2); // Set max jumps to 2, allowing double jump
            Debug.Log("Double Jump Enabled by setting max jumps to 2!");
        }
    }
}
