using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength = 5f;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private float maxGrappleDistance = 10f;
    [SerializeField] private float climbSpeed = 2f;
    [SerializeField] private float swingForce = 5f;

    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    private bool canGrapple = false;
    private HookPoint currentHookPoint = null;
    private Rigidbody2D rb;

    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        joint.enabled = false;
        rope.enabled = false;
    }

    void Update()
    {
        if (canGrapple && currentHookPoint != null && Input.GetMouseButtonDown(0))
        {
            StartGrappling();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopGrappling();
        }

        if (rope.enabled)
        {
            rope.SetPosition(1, transform.position);
        }

        if (joint.enabled && Input.GetKey(KeyCode.W))
        {
            joint.distance = Mathf.Max(0.5f, joint.distance - climbSpeed * Time.deltaTime);
        }

        if (joint.enabled && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            ApplySwingForce();
        }
    }

    public void SetCurrentHook(HookPoint hookPoint)
    {
        currentHookPoint = hookPoint;
    }

    public void SetGrappleEnabled(bool enabled)
    {
        canGrapple = enabled;
    }

    private void StartGrappling()
    {
        if (currentHookPoint != null)
        {
            grapplePoint = currentHookPoint.transform.position;
            grapplePoint.z = 0;

            joint.connectedAnchor = grapplePoint;
            joint.distance = grappleLength;
            joint.enabled = true;

            rope.enabled = true;
            rope.SetPosition(0, grapplePoint);
            rope.SetPosition(1, transform.position);
        }
    }

    private void StopGrappling()
    {
        joint.enabled = false;
        rope.enabled = false;
    }

    private void ApplySwingForce()
    {
        Vector2 directionToHook = (grapplePoint - transform.position).normalized;

        Vector2 tangentialDirection = new Vector2(-directionToHook.y, directionToHook.x);

        float inputDirection = Input.GetKey(KeyCode.A) ? -1f : 1f;

        rb.AddForce(tangentialDirection * swingForce * inputDirection, ForceMode2D.Force);
    }
}
