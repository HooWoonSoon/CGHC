using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private List<BoxController> boxs = new List<BoxController>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        BoxController boxController = other.GetComponent<BoxController>();

        if (boxController != null && !boxs.Contains(boxController))
        {
            int index = boxs.Count;  
            boxs.Add(boxController);

            BoxData.instance.AddBox(boxController.canPush, boxController.transform.position, index);
            boxController.SetBoxIndex(index);

            //BoxData.instance.CheckList();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BoxController boxController = other.GetComponent<BoxController>();

        if (boxController != null)
        {
            boxController.transform.position = BoxData.instance.GetRepawnPosition(boxController.boxIndex);
            boxController.boxStateMachine.ChangeState(boxController.boxIdleState);
        }
    }

}
