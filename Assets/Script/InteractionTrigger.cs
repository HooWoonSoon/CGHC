using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    private Interactable interactableScript;

    private void Start()
    {
        interactableScript = GetComponentInParent<Interactable>();
        if (interactableScript == null)
        {
            Debug.LogError("Interactable script not found on parent object.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactableScript.SetPlayerNearby(true);
            Debug.Log("Player entered interaction zone");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactableScript.SetPlayerNearby(false);
            Debug.Log("Player exited interaction zone");
        }
    }
}
