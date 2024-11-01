using System;
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
        BoxController boxController = other.GetComponent<BoxController>();

        if (player != null)
        {
            gameMangement.UpdateDead();
        }
        if (boxController != null)
        {
            boxController.transform.position = BoxData.instance.GetRepawnPosition(boxController.boxIndex);
            boxController.boxStateMachine.ChangeState(boxController.boxIdleState);
        }
    }
}
