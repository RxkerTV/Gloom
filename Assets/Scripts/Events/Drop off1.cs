using System.Collections;
using UnityEngine;

public class Dropoff1 : MonoBehaviour
{
    public GameObject DropOffBlock;
    public TypewriterUI typewriterUI; // Reference to TypewriterUI script

    void Start()
    {
        if (typewriterUI == null)
        {
            Debug.LogError("TypewriterUI script is not assigned.");
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
            else
            {
                Debug.Log("Has rope");
                UI.Instance.UseRope.SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Left Box");
            StartCoroutine(TextDeleteDelay());
            UI.Instance.NoRope.SetActive(false);
        }
    }

    private IEnumerator TextDeleteDelay()
    {
        yield return new WaitForSeconds(3);
        StopTypewriterEffect();
    }

    private void StartTypewriterEffect(string message)
    {
        if (typewriterUI != null)
        {
            typewriterUI.writer = message; // Set new message
            typewriterUI.StartTypewriter(); // Start typing effect
        }
    }

    private void StopTypewriterEffect()
    {
        if (typewriterUI != null)
        {
            StopAllCoroutines();
            typewriterUI.StopAllCoroutines();
            if (typewriterUI.TmpProText != null)
            {
                typewriterUI.TmpProText.text = ""; // Clear text
            }
        }
    }
}
