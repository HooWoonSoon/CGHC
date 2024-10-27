using UnityEngine;

public class HookPoint : MonoBehaviour
{
    private bool playerInRange = false;
    private GrapplingHook playerGrappling;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            playerGrappling = collision.GetComponent<GrapplingHook>();
            if (playerGrappling != null)
            {
                playerGrappling.SetCurrentHook(this);
                playerGrappling.SetGrappleEnabled(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            if (playerGrappling != null)
            {
                playerGrappling.SetCurrentHook(null);
                playerGrappling.SetGrappleEnabled(false);
                playerGrappling = null;
            }
        }
    }
}
