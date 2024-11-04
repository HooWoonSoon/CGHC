using System.Collections;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    public Animator animator; 
    private Rigidbody2D rb; 
    private AudioSource audioSource; 
    public AudioClip deathSound; 
    private bool isDead = false;
    private PlayerStateMachine stateMachine;
    private PlayerIdleState idleState; 

    private MonoBehaviour[] controlScripts; 

    private void Start()
    {
        animator = transform.Find("character-sprite_0")?.GetComponent<Animator>() ?? animator;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (animator == null) Debug.LogError("Animator not found on 'character-sprite_0'.");
        if (rb == null) Debug.LogError("Rigidbody2D component not found on the player GameObject.");
        if (audioSource == null) Debug.LogError("AudioSource component not found on the player GameObject.");

        Player player = GetComponent<Player>();
        if (player != null)
        {
            stateMachine = player.stateMachine;
            idleState = player.idleState; 
        }

        if (stateMachine == null || idleState == null)
        {
            Debug.LogError("PlayerStateMachine or PlayerIdleState not found.");
        }

        controlScripts = GetComponents<MonoBehaviour>();
    }

    public void TriggerDeath()
    {
        if (!isDead)
        {
            isDead = true;

            DisableControls();

            animator.SetTrigger("Explode");

            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true; 
            }

            if (audioSource != null && deathSound != null)
            {
                audioSource.PlayOneShot(deathSound);
            }

            StartCoroutine(HandleDeathSequence());
        }
    }

    private IEnumerator HandleDeathSequence()
    {
        if (stateMachine != null && idleState != null)
        {
            stateMachine.ChangeState(idleState);
        }
        yield return new WaitForSeconds(0.8f); 

        GameManager gameMangement = GameManager.instance;
        if (gameMangement != null)
        {
            transform.position = gameMangement.character.lastCheckpoint;
        }

        if (rb != null)
        {
            rb.isKinematic = false;
        }

        animator.SetTrigger("Respawn");
        yield return new WaitForSeconds(0.8f); 
        animator.ResetTrigger("Respawn");

        animator.Play("Idle");


        EnableControls();

        isDead = false;
    }

    private void DisableControls()
    {
        foreach (var script in controlScripts)
        {
            if (script != this)
            {
                script.enabled = false;
            }
        }
    }

    private void EnableControls()
    {
        foreach (var script in controlScripts)
        {
            if (script != this) 
            {
                script.enabled = true;
            }
        }
    }
}
