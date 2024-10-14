using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    #region Detection
    [Header("Detection")]
    [SerializeField] private Transform[] groundChecks;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask colliderWithGround;
    public Vector3 orentaition = Vector3.down;
    private float skin = 0.1f;
    #endregion

    #region state
    public BoxStateMachine boxStateMachine { get; private set; }
    public BoxIdleState boxIdleState { get; private set; }
    public BoxWeightlessnessMove boxWeightlessnessMove { get; private set; }
    #endregion

    #region components
    public Rigidbody2D rb {get; private set;}
    private BoxCollider2D boxcollider;
    #endregion

    public bool isIllumination = false;
    public PointCheck pointCheck;

    private void Awake()
    {
        boxStateMachine = new BoxStateMachine();
        boxIdleState = new BoxIdleState(boxStateMachine, this);
        boxWeightlessnessMove = new BoxWeightlessnessMove(boxStateMachine, this);
        rb = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
        pointCheck = FindObjectOfType<PointCheck>();
    }
    private void Start()
    {
        boxStateMachine.Initialize(boxIdleState);
    }

    public void Update()
    {
        boxStateMachine.currentState.Update();
    }

    private void FixedUpdate()
    {
        boxStateMachine.currentState.FixeUpdate();
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    public void ChangeGroundTranform()
    {
        Bounds boxBounds = boxcollider.bounds;

        Vector2 boundsBottomLeft = new Vector2(boxBounds.min.x, boxBounds.min.y + skin);
        Vector2 boundsBottomRight = new Vector2(boxBounds.max.x, boxBounds.min.y + skin);
        Vector2 boundsTopLeft = new Vector2(boxBounds.min.x, boxBounds.max.y - skin);
        Vector2 boundsTopRight = new Vector2(boxBounds.max.x, boxBounds.max.y - skin);
        Vector2 boundsTopLeftSide = new Vector2(boxBounds.min.x,boxBounds.max.y - skin);
        Vector2 boundsBottomLeftSide = new Vector2(boxBounds.min.x, boxBounds.min.y + skin);
        Vector2 boundsTopRightSide = new Vector2(boxBounds.max.x, boxBounds.max.y - skin);
        Vector2 boundsBottomRightSide = new Vector2(boxBounds.max.x, boxBounds.min.y + skin);
        if (orentaition == Vector3.down)
        {
            groundChecks[0].transform.position = boundsBottomLeft;
            groundChecks[1].transform.position = boundsBottomRight;
        }
        else if (orentaition == Vector3.up)
        {
            groundChecks[0].transform.position = boundsTopLeft;
            groundChecks[1].transform.position = boundsTopRight;
        }
        else if (orentaition == Vector3.left)
        {
            groundChecks[0].transform.position = boundsTopLeftSide;
            groundChecks[1].transform.position = boundsBottomLeftSide;
        }
        else if (orentaition == Vector3.right)
        {
            groundChecks[0].transform.position = boundsTopRightSide;
            groundChecks[1].transform.position = boundsBottomRightSide;
        }

    }

    public bool IsGrounded()
    {
        foreach (Transform check in groundChecks)
        {
            if (Physics2D.Raycast(check.position, orentaition, groundCheckDistance, colliderWithGround))
                return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        foreach (Transform check in groundChecks)
        {
            Debug.DrawRay(check.position, orentaition * groundCheckDistance, Color.red);
        }
    }
}
