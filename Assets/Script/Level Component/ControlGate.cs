using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGate : MonoBehaviour
{
    [SerializeField] private List<AutoGate> autoGates;
    [SerializeField] private List<Valves> valves;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        BoxController controller = other.gameObject.GetComponent<BoxController>();
        if (controller != null)
        {
            foreach (var autoGate in autoGates)
                autoGate.CloseOpenGate(true);
            foreach (var valve in valves)
                valve.swicthOn = true;
            rb.isKinematic = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        BoxController controller = other.gameObject.GetComponent<BoxController>();
        if (controller != null)
        {
            foreach (var autoGate in autoGates)
                autoGate.CloseOpenGate(false);
            foreach (var valve in valves)
                valve.swicthOn = false;
            rb.isKinematic = false;
        }
    }
}
