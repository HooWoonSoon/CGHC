using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDeathHandler deathHandler = other.GetComponent<PlayerDeathHandler>();

        if (deathHandler != null)
        {
            deathHandler.TriggerDeath();
        }
    }
}
