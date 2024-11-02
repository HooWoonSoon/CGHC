using System.Collections;
using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform firePoint; 
    public float fireRate = 2.0f; 
    private Transform player; 

    private void Start()
    {
        player = PlayerManager.instance.player.transform; 
        StartCoroutine(FireCoroutine()); 
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Fire();
        }
    }

    void Fire()
    {
        if (player != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();

            if (projectileController != null)
            {
                projectileController.SetDirection((player.position - firePoint.position).normalized);
            }
        }
    }
}
