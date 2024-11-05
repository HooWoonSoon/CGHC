using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the projectile
    private Transform target;
    public float destroyAfter = 5.0f; // Time before the projectile self-destructs

    private void Start()
    {
        Destroy(gameObject, destroyAfter); // Destroy after a set time to prevent endless flying
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }

    public void SetTarget(Transform player)
    {
        target = player;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision object is not the player
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the projectile on collision with any non-player object
        }

        // If the projectile hits the player, trigger the death handler and destroy the projectile
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDeathHandler>()?.TriggerDeath();
            Destroy(gameObject);
        }
    }
}
