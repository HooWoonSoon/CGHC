using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform shootPoint; 
    public float shootInterval = 2.0f; 
    public float detectionRange = 10.0f; 
    private bool isPlayerNearby = false;
    private Transform player;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DetectPlayer();

        if (isPlayerNearby && !IsInvoking(nameof(ShootProjectile)))
        {
            InvokeRepeating(nameof(ShootProjectile), 0f, shootInterval);
        }
        else if (!isPlayerNearby)
        {
            CancelInvoke(nameof(ShootProjectile));
        }
    }

    private void DetectPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            isPlayerNearby = distance <= detectionRange;
        }
    }

    private void ShootProjectile()
    {
        if (player != null && animator != null)
        {
            animator.SetTrigger("Shoot"); 
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Projectile projScript = projectile.GetComponent<Projectile>();

            if (projScript != null)
            {
                projScript.SetTarget(player);
            }
        }
    }
}
