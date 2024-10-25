using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private BoxCollider2D collilderBottom;
    private Vector2 endBoxOffsetBottom;
    private Vector2 originalOffsetBottom;
    private Vector2 endSizeBottom;
    private Vector2 originalSizeBottom;
    #endregion

    [SerializeField] private float openDuration = 1.0f;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        originalOffsetAbove = colliderAbove.offset;
        originalSizeAbove = colliderAbove.size;
        originalOffsetBottom = collilderBottom.offset;
        originalSizeBottom = collilderBottom.size;

        endSizeAbove = new Vector2(originalSizeAbove.x, 0);
        endBoxOffsetAbove = new Vector2(originalOffsetAbove.x, originalSizeAbove.y);

        endSizeBottom = new Vector2(originalSizeBottom.x, 0);
        endBoxOffsetBottom = new Vector2(originalOffsetBottom.x, -originalSizeBottom.y);
    }

    public void OpenGate()
    {
        anim.SetBool("Open", true);
        anim.SetBool("Close", false);
        anim.SetBool("Idle", false);

        StartCoroutine(SmoothChangeSizeAndOffset(colliderAbove, originalSizeAbove, endSizeAbove, originalOffsetAbove, endBoxOffsetAbove));
        StartCoroutine(SmoothChangeSizeAndOffset(collilderBottom, originalSizeBottom, endSizeBottom, originalOffsetBottom, endBoxOffsetBottom));

        Debug.Log("Open");
    }

    private IEnumerator SmoothChangeSizeAndOffset(BoxCollider2D collider, Vector2 startSize, Vector2 endSize, Vector2 startOffset, Vector2 endOffset)
    {
        float elapsedTime = 0f;
        while (elapsedTime < openDuration)
        {
            collider.size = Vector2.Lerp(startSize, endSize, elapsedTime / openDuration);
            collider.offset = Vector2.Lerp(startOffset, endOffset, elapsedTime / openDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        collider.size = endSize;
        collider.offset = endOffset;
    }

    public void CloseGate()
    {
        anim.SetBool("Open", false);
        anim.SetBool("Close", true);
        anim.SetBool("Idle", false);

        StartCoroutine(SmoothChangeSizeAndOffset(colliderAbove, endSizeAbove, originalSizeAbove, endBoxOffsetAbove, originalOffsetAbove));
        StartCoroutine(SmoothChangeSizeAndOffset(collilderBottom, endSizeBottom, originalSizeBottom, endBoxOffsetBottom, originalOffsetBottom));
        Debug.Log("Close");
    }
}
