using UnityEngine;
using System.Collections;

public class LaserTrap : MonoBehaviour
{
    [SerializeField] private float activeDuration = 3f; 
    [SerializeField] private float inactiveDuration = 3f; 
    private Animator animator;
    private bool isLaserActive = true; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
            return;
        }
        StartCoroutine(LaserCycle());
    }

    private IEnumerator LaserCycle()
    {
        while (true)
        {
            isLaserActive = true;
            animator.SetTrigger("Idle"); 
            yield return new WaitForSeconds(activeDuration);

            isLaserActive = false;
            animator.SetTrigger("Deactivate");
            yield return new WaitForSeconds(1f);

            yield return new WaitForSeconds(inactiveDuration);

            animator.SetTrigger("Activate");
            yield return new WaitForSeconds(1f); 

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLaserActive && collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDeathHandler>()?.TriggerDeath();
        }
    }
}
