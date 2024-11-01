using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Portal : MonoBehaviour
{
    [Header("Scene to Load")]
    [SerializeField] private string sceneName; 
    public Image promptImage;
    public Image fadeImage; 

    private bool playerIsNearby = false;

    private void Start()
    {
        if (promptImage != null)
        {
            promptImage.gameObject.SetActive(false); 
        }

        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNearby = true;
            if (promptImage != null)
            {
                promptImage.gameObject.SetActive(true); 
                Debug.Log("Showing prompt image");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNearby = false;
            if (promptImage != null)
            {
                promptImage.gameObject.SetActive(false);
                Debug.Log("Hiding prompt image");
            }
        }
    }


    private void Update()
    {
        if (playerIsNearby)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("Teleporting to scene: " + sceneName);
                StartCoroutine(FadeOutAndLoadScene());
            }
        }
    }


    private IEnumerator FadeOutAndLoadScene()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            float fadeDuration = 1.0f; 
            float time = 0;

            while (time < fadeDuration)
            {
                fadeImage.color = new Color(0, 0, 0, time / fadeDuration);
                time += Time.deltaTime;
                yield return null;
            }

            fadeImage.color = new Color(0, 0, 0, 1); 

            SceneManager.LoadScene(sceneName);
        }
    }
}
