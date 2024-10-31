using UnityEngine;
using Unity.Cinemachine;

public class RoomBoundarySwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _virtualCamera;
    private CinemachineConfiner2D _confiner;

    private void Awake()
    {
        _confiner = _virtualCamera.GetComponent<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Confiner"))
        {
            BoxCollider2D newBoundary = collision.GetComponent<BoxCollider2D>();

            if (newBoundary != null)
            {
                _confiner.BoundingShape2D = newBoundary;
                _confiner.InvalidateBoundingShapeCache();
            }
            else
            {
                Debug.LogError("BoxCollider2D not found on the collided object.");
            }
        }
    }
}
