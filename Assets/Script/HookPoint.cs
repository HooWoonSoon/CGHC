using UnityEngine;

public class HookPoint : MonoBehaviour
{
    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            playerInRange = true;
            player.SetCurrentHook(this);
            player.SetGrappleEnabled(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            playerInRange = false;

            player.SetCurrentHook(null);
            player.SetGrappleEnabled(false);
            player = null;
            
        }
    }
}
