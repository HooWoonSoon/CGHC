using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGate : MonoBehaviour
{
    [SerializeField] private AutoGate autoGate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        BoxController controller = other.GetComponentInParent<BoxController>();
        if (controller != null)
        {
            autoGate.OpenGate();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BoxController controller = other.GetComponentInParent<BoxController>();
        if (controller != null)
        {
            autoGate.CloseGate();
        }
    }
}
