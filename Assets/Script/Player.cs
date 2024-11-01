﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [Header("Action")]
    public float moveSpeed = 2.5f;
    public float jumpForce = 2.5f;
    public int maxJumps = 1;
    public int leftJump;
    private bool canDoubleJump;

    [Header("Dash")]
    public float dashSpeed;
    public float dashDuration;
    private bool canDash;
    public bool isDash;
    public float dashDirection {  get; private set; }

    [Header("Hook")]
    [SerializeField] private float grappleLength = 5f;
    [SerializeField] private float maxGrappleDistance = 10f;
    public float climbSpeed = 2f;
    [SerializeField] private float swingForce = 5f;
    public LineRenderer rope;

    private Vector3 grapplePoint;
    private HookPoint currentHookPoint = null;
    private bool canGrapple = false;

    [Header("Collision")]
    [SerializeField] private LayerMask colliderWithGround;
    [SerializeField] private LayerMask colliderWithBox;
    [SerializeField] private int verticalRayAmount = 4;
    [SerializeField] private int horizontalRayAmount = 4;
    [SerializeField] private Transform gravityControlCheck;
    [SerializeField] private float gravityControlCheckRadius;
    [SerializeField] private Transform pushControlCheck;
    [SerializeField] private float pushCheckDistance;
    [SerializeField] private Transform exitPushCheck;
    [SerializeField] private float exitPushDistance;

    public int facingDirection { get; private set; } = 1;
    public bool facingRight { get; private set; } = true;

    #region component
    public BoxController previousBox {  get; private set; }
    public BoxController boxController { get; private set; }
    private RoomManagement boxManagement;
    public GameObject hitBox;
    public GameObject ControlEffect;
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public DistanceJoint2D joint { get; private set; }
    private CapsuleCollider2D capsulecollider;
    #endregion


    #region state
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPushState pushState { get; private set; }
    public PlayerGravityControlState gravityControlState { get; private set; }
    public PlayerHookState hookState { get; private set; }
    #endregion

    #region internal
    private Vector2 boundsTopLeft;
    private Vector2 boundsTopRight;
    private Vector2 boundsBottomLeft;
    private Vector2 boundsBottomRight;
    private Vector2 boundsMiddleLeft;
    private Vector2 boundsMiddleRight;

    public float boundsWidth;
    //private float boundsHeight;
    public float skin { get; private set; } = 0.05f;
    public bool isGrounded { get; private set; }
    public bool isFloors { get; private set; }

    public bool isPushing = false;
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        airState = new PlayerAirState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
        wallSlideState = new PlayerWallSlideState(stateMachine, this, "WallSlide");
        wallJumpState = new PlayerWallJumpState(stateMachine, this, "Jump");
        pushState = new PlayerPushState(stateMachine, this, "Push");
        gravityControlState = new PlayerGravityControlState(stateMachine, this, "Control");
        hookState = new PlayerHookState(stateMachine, this, "Hook");
    }

    private void Start()
    {
        capsulecollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stateMachine.Initialize(idleState);
        leftJump = maxJumps;
        ControlEffect.SetActive(false);
        boxManagement = FindAnyObjectByType<RoomManagement>();
        joint = gameObject.GetComponent<DistanceJoint2D>();
        rope.enabled = false;
    }

    private void Update()
    {
        stateMachine.currentState.Update();

        CheckForDashInput();
        CheckForHookInput();
        SetRayOrigins();
        CollisionBelowAndAbove();
        BoxGravityControlDetected();
        if (BoxGravityControlDetected() == false)
        {
            if (previousBox != null)
            {
                previousBox.SlideLight(false); // Use to fixed the wicked ASS HOLE, I know this is very stupid!!!
                previousBox.GravityOrientation(false); // I give up even if GPT also couldn't help
            }
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

    private void CheckForHookInput()
    {
        if (canGrapple && currentHookPoint != null && Input.GetMouseButtonDown(0))
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
            stateMachine.ChangeState(hookState);
        }
    }

    public void ApplySwingForce()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("clockwise");
            rb.AddForce(Vector2.right * swingForce, ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("counterclockwise");
            rb.AddForce(Vector2.left * swingForce, ForceMode2D.Impulse);
        }
    }

    public void CheckForJumpCount(bool IsGround)
    {
        if (IsGround)
        {
            leftJump = maxJumps;
        }
        else
        {
            leftJump -= 1;
        }
    }

    private void CheckForDashInput()
    {
        if (HorizontalWallDetected())
        {
            return;
        }
        else if (isGrounded && !isDash)
        {
            canDash = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            dashDirection = Input.GetAxisRaw("Horizontal");
            if (dashDirection == 0)
            {
                dashDirection = facingDirection;
            }
            canDash = false;
            stateMachine.ChangeState(dashState);
        }
    }

    public void AniamtionTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    #region Ray Origin
    private void SetRayOrigins()
    {
        Bounds playerBounds = capsulecollider.bounds;

        boundsBottomLeft = new Vector2(playerBounds.min.x, playerBounds.min.y);
        boundsBottomRight = new Vector2(playerBounds.max.x, playerBounds.min.y);
        boundsTopLeft = new Vector2(playerBounds.min.x, playerBounds.max.y);
        boundsTopRight = new Vector2(playerBounds.max.x, playerBounds.max.y);
        boundsMiddleLeft = new Vector2(playerBounds.min.x, (playerBounds.min.y + playerBounds.max.y) / 2);
        boundsMiddleRight = new Vector2(playerBounds.max.x, (playerBounds.min.y + playerBounds.max.y) / 2);
    }
    #endregion

    #region Wall And Ground Check
    private void CollisionBelowAndAbove()
    {
        float baseRayLength = Vector2.Distance(boundsMiddleLeft, boundsTopLeft) + skin;
        float bottomRayLength = baseRayLength;
        float aboveRayLength = baseRayLength;
        switch (rb.velocity.y)
        {
            case < -0.01f:
                bottomRayLength += 0.1f;
                break;
            case > 0.01f:
                aboveRayLength += 0.1f;
                break;
        }
        this.isGrounded = CheckCollision(-transform.up, bottomRayLength, Color.green, out bool isGrounded);
        this.isFloors = CheckCollision(transform.up, aboveRayLength, Color.red, out bool isFloors);
    }

    private bool CheckCollision(Vector2 direction, float rayLength, Color debugColor, out bool isHit)
    {
        isHit = false;
        for (int i = 0; i < verticalRayAmount; i++)
        {
            Vector2 rayOrigin = Vector2.Lerp(boundsMiddleLeft, boundsMiddleRight, (float)i / (verticalRayAmount - 1));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, rayLength, colliderWithGround);
            Debug.DrawRay(rayOrigin, direction * rayLength, debugColor);
            if (hit)
            {
                isHit = true;
                break;
            }
        }
        return isHit;
    }

    public bool HorizontalWallDetected()
    {
        boundsWidth = Vector2.Distance(boundsMiddleLeft, boundsMiddleRight);
        float speedFactor = Mathf.Abs(rb.velocity.x);
        float minRayLength = skin;
        float maxRayLength = minRayLength * 2;
        float rayLength = Mathf.Lerp(minRayLength, maxRayLength, speedFactor / dashSpeed);

        Vector2 startOrigin, endOrigin;
        Vector2 direction;

        if (facingRight)
        {
            startOrigin = boundsTopRight;
            endOrigin = boundsBottomRight;
            direction = transform.right;
        }
        else
        {
            startOrigin = boundsTopLeft;
            endOrigin = boundsBottomLeft;
            direction = -transform.right;
        }

        for (int i = 0; i < horizontalRayAmount; i++)
        {
            Vector2 rayOrigin = Vector2.Lerp(startOrigin, endOrigin, (float)i / (horizontalRayAmount - 1));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, rayLength, colliderWithGround);

            Debug.DrawRay(rayOrigin, direction * rayLength, Color.yellow);

            if (hit)
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    public bool IsBoxDetected() 
    {
        RaycastHit2D hit = Physics2D.Raycast(pushControlCheck.position, Vector2.right * facingDirection, pushCheckDistance, colliderWithBox);

        if (hit)
        {
            hitBox = hit.collider.gameObject;
            return true;
        }
        return false;
    }

    public bool ExitForPushDetected() => Physics2D.Raycast(exitPushCheck.position, Vector2.down, exitPushDistance, colliderWithGround);

    public bool BoxGravityControlDetected()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gravityControlCheck.position, gravityControlCheckRadius,colliderWithBox);
        float minDistance = Mathf.Infinity;
        boxController = null;
        foreach (var hit in colliders)
        {
            float distance = Vector2.Distance(hit.transform.position, this.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                boxController = hit.gameObject.GetComponent<BoxController>();
                boxController.SlideLight(true);
            }
 
            if (previousBox != boxController)
            {
                if (previousBox != null)
                {
                    previousBox.SlideLight(false);
                }
                previousBox = boxController;
            }
        }
        return boxController != null;
    }

    public void KillPlayer()
    {
        Debug.Log("Kill Player");
    }

    #region Flip
    public void Flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.localScale = new Vector3(facingDirection, 1, 1);
    }

    public void FlipController(float x)
    {
        if (isPushing)
        {
            return;
        }
        if (x < 0 && facingRight)
        {
            Flip();
        }
        else if (x > 0 && !facingRight)
        {
            Flip();
        }
    }
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gravityControlCheck.position, gravityControlCheckRadius);
        Gizmos.DrawLine(pushControlCheck.position, pushControlCheck.position + Vector3.right * facingDirection * pushCheckDistance);
        Gizmos.DrawLine(exitPushCheck.position, exitPushCheck.position + Vector3.down * pushCheckDistance);
    }
}
