using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadArea : MonoBehaviour
{
    private GameManager gameMangement;

    private void Start()
    {
        gameMangement = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDeathHandler deathHandler = other.GetComponent<PlayerDeathHandler>();
        BoxController boxController = other.GetComponent<BoxController>();

        if (deathHandler != null)
        {
            deathHandler.TriggerDeath();
            gameMangement.UpdateDead();
        }
        if (boxController != null)
        {
            boxController.transform.position = BoxData.instance.GetRepawnPosition(boxController.boxIndex);
            boxController.boxStateMachine.ChangeState(boxController.boxIdleState);
        }
    }
}
