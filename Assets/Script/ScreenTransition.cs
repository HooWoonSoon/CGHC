using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage; 
    public float fadeDuration = 1.0f;

    private void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.gameObject.SetActive(false);
    }

    public IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
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

    public IEnumerator FadeIn()
    {
        float time = fadeDuration;
        fadeImage.color = new Color(0, 0, 0, 1); 

        while (time > 0)
        {
            fadeImage.color = new Color(0, 0, 0, time / fadeDuration);
            time -= Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0); 
        fadeImage.gameObject.SetActive(false);
    }
}
