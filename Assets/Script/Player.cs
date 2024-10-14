using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public float dashSpeed;
    public float dashDuration;
    private bool canDash;
    public bool isDash;
    private bool canDoubleJump;
    public float dashDirection {  get; private set; }

    [Header("Collision")]
    [SerializeField] private LayerMask colliderWithGround;
    public LayerMask colliderWithBox;
    [SerializeField] private int verticalRayAmount = 4;
    [SerializeField] private int horizontalRayAmount = 4;
    public Transform controlCheck;
    public float controlCheckRadius;
    
    public Transform BoxPointTransform;
    public GameObject hitBox;
    public int facingDirection { get; private set; } = 1;
    public bool facingRight { get; private set; } = true;

    #region component
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    private CapsuleCollider2D capsulecollider;
    public Pushable pushable;
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
    public bool isWalled { get; private set; }
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
    }

    private void Start()
    {
        capsulecollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stateMachine.Initialize(idleState);
        pushable = FindObjectOfType<Pushable>();
        leftJump = maxJumps;
    }

    private void Update()
    {
        stateMachine.currentState.Update();

        CheckForDashInput();
    }

    private void FixedUpdate()
    {
        SetRayOrigins();
        CollisionBelowAndAbove();
        HorizontalCollision();
        ControlTrigger();
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
        if (isWalled)
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

    private void HorizontalCollision()
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

        isWalled = false;


        for (int i = 0; i < horizontalRayAmount; i++)
        {
            Vector2 rayOrigin = Vector2.Lerp(startOrigin, endOrigin, (float)i / (horizontalRayAmount - 1));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, rayLength, colliderWithGround);

            Debug.DrawRay(rayOrigin, direction * rayLength, Color.yellow);

            if (hit)
            {
                isWalled = true;
                break;
            }
        }
    }

    public bool ControlTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(controlCheck.position, controlCheckRadius, colliderWithBox);

        foreach (var hit in colliders)
        {
            hitBox = hit.gameObject;
            Illumination illumination = hit.GetComponentInChildren<Illumination>();
            if (illumination != null)
            {    
                illumination.SlideLight(true);
                return true;
            }
        }
        return false;
    }

    public void KillPlayer()
    {
        Debug.Log("Kill Player");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(controlCheck.position, controlCheckRadius);
    }

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
}
