using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class Dropoff1 : MonoBehaviour
{
    public GameObject DropOffBlock;
    public GameObject DropOffPrompt;
    public GameObject RockToAttach;
    public TypewriterUI typewriterUI;
    public bool UseRopeText;

    public Image fadeImage; // Reference to the UI Image for fading
    public float fadeDuration = 2.5f; // Duration of the fade

    void Start()
    {
        if (typewriterUI == null)
        {
            Debug.LogError("TypewriterUI script is not assigned.");
        }

        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 0); // Ensure it's initially transparent
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Ropescript.Instance.HasRope == false)
            {
                Debug.Log("Doesn't have rope");
                UI.Instance.NoRope.SetActive(true);
                StartTypewriterEffect("You need a rope to use this.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TextDeleteDelay());
        }
        if (other.CompareTag("Player") && UseRopeText == true)
        {
            UI.Instance.UseRope.SetActive(false);
        }
    }

    private IEnumerator TextDeleteDelay()
    {
        yield return new WaitForSeconds(2.5f);
        UI.Instance.NoRope.SetActive(false);
    }

    private void StartTypewriterEffect(string message)
    {
        if (typewriterUI != null)
        {
            typewriterUI.writer = message; // Set new message
            typewriterUI.StartTypewriter(); // Start typing effect
        }
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 5.5f, PlayerCam.Instance.interactableLayer))
        {
            if (hit.collider.gameObject == RockToAttach)
            {
                // Check for interaction input
                if  (Input.GetButtonDown("Interact"))
                {
                    UI.Instance.interactText.SetActive(false);
                    StartCoroutine(FadeToBlack()); // Start fading to black
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
}