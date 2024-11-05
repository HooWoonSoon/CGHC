using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private List<BoxController> boxs = new List<BoxController>();
    private BoxCollider2D boxCollider;
    private Vector2 center;
    private Vector2 size;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        center = boxCollider.bounds.center;
        size = boxCollider.bounds.size;
        BoxsInRoom();
    }

    private void Update()
    {
        Collider2D[] collidersInRange = Physics2D.OverlapBoxAll(center, size, 0f);

        foreach (Collider2D collider in collidersInRange)
        {
            PlayerDeathHandler deathHandler = collider.GetComponent<PlayerDeathHandler>();
            if (deathHandler != null && deathHandler.isDead == true)
            {
                foreach (BoxController boxController in boxs)
                {
                    RespawnBox(boxController);
                }
            }
        }
    }


    private void BoxsInRoom()
    {
        Collider2D[] collidersInRange = Physics2D.OverlapBoxAll(center, size, 0f);

        foreach (Collider2D collider in collidersInRange)
        {
            BoxController boxController = collider.GetComponent<BoxController>();
            if (boxController != null)
            {
                boxs.Add(boxController);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BoxController boxController = other.GetComponent<BoxController>();

        if (boxController != null)
        {
            RespawnBox(boxController);
        }
    }

    private void RespawnBox(BoxController boxController)
    {
        boxController.boxStateMachine.ChangeState(boxController.boxIdleState);
        boxController.transform.position = BoxData.instance.GetRepawnPosition(boxController.boxIndex);
        boxController.orientation = BoxData.instance.GetRepawnOrientation(boxController.boxIndex);
    }
}
