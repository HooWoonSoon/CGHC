using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    private bool isDialogOpen = false;
    private Coroutine displayCoroutine;
    private Queue<string> dialogQueue = new Queue<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
        }
    }

    public void ShowDialog(string text)
    {
        if (dialogBox == null || dialogText == null)
        {
            Debug.LogError("DialogBox or DialogText is not assigned in the Inspector");
            return;
        }

        dialogQueue.Enqueue(text);
        if (!isDialogOpen)
        {
            DisplayNextDialog();
        }
    }

    private void DisplayNextDialog()
    {
        if (dialogQueue.Count > 0)
        {
            string text = dialogQueue.Dequeue();
            dialogBox.SetActive(true); 
            if (displayCoroutine != null)
            {
                StopCoroutine(displayCoroutine);
            }
            displayCoroutine = StartCoroutine(DisplayTextCharacterByCharacter(text));
            isDialogOpen = true;
        }
        else
        {
            if (isDialogOpen)
            {
                HideDialog();
            }
        }
    }

    public void HideDialog()
    {
        if (dialogBox == null)
        {
            Debug.LogError("DialogBox is not assigned in the Inspector");
            return;
        }

        dialogBox.SetActive(false); 
        isDialogOpen = false;
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
            displayCoroutine = null;
        }

        if (dialogQueue.Count > 0)
        {
            Invoke("DisplayNextDialog", 0.1f);
        }
    }

    private IEnumerator DisplayTextCharacterByCharacter(string text)
    {
        dialogText.text = "";
        foreach (char c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void Update()
    {
        if (isDialogOpen && Input.GetMouseButtonDown(0))
        {
            HideDialog();
        }
    }
}
