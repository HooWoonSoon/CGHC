using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGate : MonoBehaviour
{
    [SerializeField] private AutoGate autoGate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BoxController boxController = collision.GetComponentInParent<BoxController>();

        if (boxController != null)
        {
            autoGate.OpenGate();
            Debug.Log(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Player"))
        {
            autoGate.CloseGate();
            Debug.Log(false);
        }
    }
}
