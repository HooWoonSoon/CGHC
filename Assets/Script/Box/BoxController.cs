using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    #region Detection
    [Header("Detection")]
    [SerializeField] private Transform[] vasicoisChecks;
    [SerializeField] private Transform[] groundChecks;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask colliderWithGround;
    public Vector3 orentaition = Vector3.down;
    private float skin = 0.1f;
    #endregion

    #region state
    public BoxStateMachine boxStateMachine { get; private set; }
    public BoxIdleState boxIdleState { get; private set; }
    public BoxAirState boxAirState { get; private set; }
    public BoxWeightlessnessMove boxWeightlessnessMove { get; private set; }
    #endregion

    #region components
    public Rigidbody2D rb {get; private set;}
    private BoxCollider2D boxcollider;
    #endregion

    public bool isIllumination = false;
    public Vector3 wayMove { get; private set; }
    public bool canPush {  get; private set; }
    public int boxIndex { get; private set; }
    [SerializeField] private GameObject pointer;

    private void Awake()
    {
        boxStateMachine = new BoxStateMachine();
        boxIdleState = new BoxIdleState(boxStateMachine, this);
        boxAirState = new BoxAirState(boxStateMachine, this);
        boxWeightlessnessMove = new BoxWeightlessnessMove(boxStateMachine, this);
        rb = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        boxStateMachine.Initialize(boxIdleState);
    }

    public void Update()
    {
        boxStateMachine.currentState.Update();
        GravityOrientation();
    }

    private void FixedUpdate()
    {
        boxStateMachine.currentState.FixeUpdate();
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    public void SetBoxIndex(int Index)
    {
        boxIndex = Index;
        Debug.Log(boxIndex);
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
            vasicoisChecks[0].transform.position = boundsBottomLeft;
            vasicoisChecks[1].transform.position = boundsBottomRight;
        }
        else if (orentaition == Vector3.up)
        {
            vasicoisChecks[0].transform.position = boundsTopLeft;
            vasicoisChecks[1].transform.position = boundsTopRight;
        }
        else if (orentaition == Vector3.left)
        {
            vasicoisChecks[0].transform.position = boundsTopLeftSide;
            vasicoisChecks[1].transform.position = boundsBottomLeftSide;
        }
        else if (orentaition == Vector3.right)
        {
            vasicoisChecks[0].transform.position = boundsTopRightSide;
            vasicoisChecks[1].transform.position = boundsBottomRightSide;
        }
    }

    public bool GroundDetected()
    {
        foreach (Transform check in groundChecks)
        {
            if (Physics2D.Raycast(check.position, Vector2.down, checkDistance, colliderWithGround))
                return true;
        }
        return false;
    }

    public bool VasicouisDetected()
    {
        foreach (Transform check in vasicoisChecks)
        {
            if (Physics2D.Raycast(check.position, orentaition, checkDistance, colliderWithGround))
                return true;
        }
        return false;
    }

    private void GravityOrientation()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = mousePosition - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                pointer.transform.rotation = Quaternion.Euler(0, 0, 270);
                wayMove = Vector3.right;
            }
            else
            {
                pointer.transform.rotation = Quaternion.Euler(0, 0, 90);
                wayMove = Vector3.left;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                pointer.transform.rotation = Quaternion.Euler(0, 0, 0);
                wayMove = Vector3.up;
            }
            else
            {
                pointer.transform.rotation = Quaternion.Euler(0, 0, 180);
                wayMove = Vector3.down;
            }
        }
    }
    private void OnDrawGizmos()
    {
        foreach (Transform check in vasicoisChecks)
        {
            Debug.DrawRay(check.position, orentaition * checkDistance, Color.red);
        }
        foreach (Transform check in groundChecks)
        {
            Debug.DrawRay(check.position, Vector2.down * checkDistance, Color.blue);
        }
    }
}
