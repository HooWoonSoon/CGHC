using UnityEngine;

public class Interactable : MonoBehaviour
{
    [TextArea]
    public string dialogText;

    private bool isPlayerNearby;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            DialogManager.Instance.ShowDialog(dialogText);
        }
    }

    public void SetPlayerNearby(bool isNearby)
    {
        isPlayerNearby = isNearby;
    }
}
