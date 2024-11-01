using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.XR;

public class BoxController : MonoBehaviour
{
    #region Detection
    [Header("Detection")]
    [SerializeField] private Transform[] vasicoisChecks;
    [SerializeField] private Transform[] groundChecks;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask colliderWithGround;
    public Vector3 orentaition = Vector3.down;
    private float skin = 0.05f;
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
    private SpriteRenderer sr;
    private Material originalMt;
    [SerializeField] Material changeMt;
    private Player player;
    private BoxData boxData;
    #endregion

    public Vector3 wayMove { get; private set; }
    public bool canPush;
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
        player = FindAnyObjectByType<Player>();
        boxStateMachine.Initialize(boxIdleState);
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMt = sr.material;
        pointer.SetActive(false);
        boxData = BoxData.instance;
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

    public void SetBoxIndex(int Index)
    {
        boxIndex = Index;
    }

    public void ChangeVasicoisTranform()
    {
        Bounds boxBounds = boxcollider.bounds;

        Vector3 boundsBottomLeft = new Vector3(boxBounds.min.x + skin, boxBounds.min.y + skin, 0f);
        Vector3 boundsBottomRight = new Vector3(boxBounds.max.x - skin, boxBounds.min.y + skin, 0f);
        Vector3 boundsTopLeft = new Vector3(boxBounds.min.x + skin, boxBounds.max.y - skin, 0f);
        Vector3 boundsTopRight = new Vector3(boxBounds.max.x - skin, boxBounds.max.y - skin, 0f);
        Vector3 boundsTopLeftSide = new Vector3(boxBounds.min.x, boxBounds.max.y - skin * 2, 0f);
        Vector3 boundsBottomLeftSide = new Vector3(boxBounds.min.x, boxBounds.min.y + skin * 2, 0f);
        Vector3 boundsTopRightSide = new Vector3(boxBounds.max.x, boxBounds.max.y - skin * 2, 0f);
        Vector3 boundsBottomRightSide = new Vector3(boxBounds.max.x, boxBounds.min.y + skin * 2, 0f);

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

    //public void UpdateDistance(float distance)
    //{
    //    boxData.UpdateDistance(distance, boxIndex);
    //    boxData.CheckList();
    //}
    public bool VasicouisDetected()
    {
        foreach (Transform check in vasicoisChecks)
        {
            if (Physics2D.Raycast(check.position, orentaition, checkDistance, colliderWithGround))
                return true;
        }
        return false;
    }

    public void GravityOrientation(bool Show)
    {
        pointer.SetActive(Show);
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

    public void SlideLight(bool isIlluminate)
    {
        if (isIlluminate)
            sr.material = changeMt;
        else sr.material = originalMt;
    }

    private void OnDrawGizmos()
    {
        foreach (Transform check in vasicoisChecks)
        {
            Gizmos.color = Color.yellow;
            Debug.DrawRay(check.position, orentaition * checkDistance, Color.red);
        }
        foreach (Transform check in groundChecks)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(check.position, new Vector3(check.position.x, check.position.y - checkDistance));
        }
    }
}
