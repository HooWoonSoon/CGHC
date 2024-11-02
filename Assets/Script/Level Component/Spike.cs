using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private GameManager gameMangement;
    private void Start()
    {
        gameMangement = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDeathHandler deathHandler = other.GetComponent<PlayerDeathHandler>();

        if (deathHandler != null)
        {
            deathHandler.TriggerDeath();
            gameMangement.UpdateDead();
        }
    }
}
