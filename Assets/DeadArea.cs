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
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            gameMangement.UpdateDead();
            player.transform.position = gameMangement.character.lastCheckpoint;
        }
    }
}
