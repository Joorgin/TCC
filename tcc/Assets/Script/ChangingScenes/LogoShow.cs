using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoShow : MonoBehaviour
{
    public Image[] images;          // Array of Images to fade in and out
    public float fadeDuration = 1f; // Time it takes to fade in or out
    public float waitDuration = 1f; // Time to wait before fading out

    private void Start()
    {
        StartCoroutine(FadeImagesInSequence());
    }

    private IEnumerator FadeImagesInSequence()
    {
        foreach (Image image in images)
        {
            // Fade In
            yield return StartCoroutine(FadeImage(image, 0f, 1f));

            // Wait
            yield return new WaitForSeconds(waitDuration);

            // Fade Out
            yield return StartCoroutine(FadeImage(image, 1f, 0f));
            SceneManager.LoadScene("Menu");
        }
    }

    private IEnumerator FadeImage(Image image, float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            image.color = color; // Update the alpha value
            yield return null;
        }

        color.a = endAlpha; // Ensure the alpha is set to the final value
        image.color = color;
    }
}
