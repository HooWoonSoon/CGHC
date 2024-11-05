using UnityEngine;

public class AutoGate : MonoBehaviour
{
    private Animator anim;

    #region Above
    [SerializeField] private BoxCollider2D colliderAbove;
    private Vector2 endBoxOffsetAbove;
    private Vector2 originalOffsetAbove;
    private Vector2 endSizeAbove;
    private Vector2 originalSizeAbove;
    #endregion

    #region Bottom
    [SerializeField] private BoxCollider2D colliderBottom;
    private Vector2 endBoxOffsetBottom;
    private Vector2 originalOffsetBottom;
    private Vector2 endSizeBottom;
    private Vector2 originalSizeBottom;
    #endregion

    [SerializeField] private float openDuration = 1.0f;

    private bool isGateOpen = false;
    private float elapsedTime = 0f;
    private bool isAnimating = false;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();

        originalOffsetAbove = colliderAbove.offset;
        originalSizeAbove = colliderAbove.size;
        originalOffsetBottom = colliderBottom.offset;
        originalSizeBottom = colliderBottom.size;

        endSizeAbove = new Vector2(originalSizeAbove.x, 0);
        endBoxOffsetAbove = new Vector2(originalOffsetAbove.x, originalSizeAbove.y);

        endSizeBottom = new Vector2(originalSizeBottom.x, 0);
        endBoxOffsetBottom = new Vector2(originalOffsetBottom.x, -originalSizeBottom.y);
    }

    private void Update()
    {
        if (!isAnimating) return;

        elapsedTime += Time.deltaTime;
        float time = Mathf.Clamp01(elapsedTime / openDuration);

        if (isGateOpen)
        {
            colliderAbove.size = Vector2.Lerp(originalSizeAbove, endSizeAbove, time);
            colliderAbove.offset = Vector2.Lerp(originalOffsetAbove, endBoxOffsetAbove, time);
            colliderBottom.size = Vector2.Lerp(originalSizeBottom, endSizeBottom, time);
            colliderBottom.offset = Vector2.Lerp(originalOffsetBottom, endBoxOffsetBottom, time);
        }
        else
        {
            colliderAbove.size = Vector2.Lerp(endSizeAbove, originalSizeAbove, time);
            colliderAbove.offset = Vector2.Lerp(endBoxOffsetAbove, originalOffsetAbove, time);
            colliderBottom.size = Vector2.Lerp(endSizeBottom, originalSizeBottom, time);
            colliderBottom.offset = Vector2.Lerp(endBoxOffsetBottom, originalOffsetBottom, time);
        }

        if (time >= 1f)
        {
            elapsedTime = 0f;
            isAnimating = false; 
        }
    }

    public void CloseOpenGate(bool Open)
    {
        anim.SetBool("Open", Open);
        anim.SetBool("Close", !Open);
        isGateOpen = Open;
        elapsedTime = 0f;
        isAnimating = true; 
    }
}

