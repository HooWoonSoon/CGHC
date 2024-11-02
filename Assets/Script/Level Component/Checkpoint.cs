using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gameMangement;

    private void Start()
    {
        gameMangement = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            gameMangement.UpdateCheckpoint(player.transform.position);
        }
    }
}
