using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Dropoff1 : MonoBehaviour
{
    public GameObject DropOffBlock;
    public GameObject DropOffPrompt;
    public GameObject RockToAttach;
    public TypewriterUI typewriterUI;
    public bool UseRopeText;

   

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




}