using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseRope : MonoBehaviour
{
    public Image fadeImage; // Reference to the UI Image for fading
    public float fadeDuration = 2.5f; // Duration of the fade

    // Start is called before the first frame update
    void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 0); // Ensure it's initially transparent
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 2.5f, PlayerCam.Instance.interactableLayer))
        {
            if (hit.collider.gameObject == gameObject)
            {
                PlayerCam.Instance.inReach = true;
                // Check for interaction input
                if (Input.GetButtonDown("Interact"))
                {
                    UI.Instance.interactText.SetActive(false);
                    StartCoroutine(FadeToBlack()); // Start fading to black
                    // Optionally, you can start fading back to transparent after some delay
                    StartCoroutine(FadeBackSequence());
                }
            }
        }
    }

    private IEnumerator FadeToBlack()
    {
        if (fadeImage == null)
            yield break;

        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Ensure it is fully black
        color.a = 1f;
        fadeImage.color = color;
    }

    private IEnumerator FadeBackSequence()
    {
        yield return new WaitForSeconds(1f); // Wait 1 second after fading to black
        StartCoroutine(FadeToTransparent());
    }

    private IEnumerator FadeToTransparent()
    {
        if (fadeImage == null)
            yield break;

        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }

        // Ensure it is fully transparent
        color.a = 0f;
        fadeImage.color = color;
    }
}