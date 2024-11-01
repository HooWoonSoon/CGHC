using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadArea : MonoBehaviour
{
    private GameMangement gameMangement;

    private void Start()
    {
        gameMangement = FindAnyObjectByType<GameMangement>();
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
