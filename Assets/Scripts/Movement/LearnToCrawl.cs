using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnToCrawl : MonoBehaviour
{
    public typewriterUI typewriterUI;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                Debug.Log("Doesn't have rope");
                UI.Instance.NoRope.SetActive(true);
                StartTypewriterEffect("You need a rope to use this.");
            
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
