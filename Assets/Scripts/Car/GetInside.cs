using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetInside : MonoBehaviour
{
    public LayerMask interactableLayer;
    public GameObject player;
    public Transform carSeatPosition;
    public float fadeDuration = 2.5f;
    public Image fadeImage;

    private bool inReach = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            UI.Instance.interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            UI.Instance.interactText.SetActive(false);
        }
    }



    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 2.5f, interactableLayer))
        {
            if (hit.collider.gameObject == gameObject) // Check if the raycast hit this door
            {
                inReach = true;
                if (inReach && Input.GetButtonDown("Interact"))
                {
                    StartCoroutine(FadeToBlackAndMove());
                }
            }
           
        }
    }

    private IEnumerator FadeToBlackAndMove()
    {
        // Fade to black
        yield return StartCoroutine(FadeImage(1f));

        // Move the character into the car
        player.transform.position = carSeatPosition.position;
        player.transform.rotation = carSeatPosition.rotation;

        // Fade back to clear
        yield return StartCoroutine(FadeImage(0f));
    }

    private IEnumerator FadeImage(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        // Ensure the target alpha is set at the end
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
    }
}
