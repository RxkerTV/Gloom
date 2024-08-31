using System.Collections;
using UnityEngine;
using TMPro;

public class typewriterUI : SingletonMonoBehaviour<typewriterUI>
{
    public TMP_Text tmpProText; // Serialized field to assign via Inspector
    public bool typing;
    public TMP_Text TmpProText
    {
        get { return tmpProText; }
        set { tmpProText = value; }
    }

    public string writer; // Public to allow setting from other scripts
    [SerializeField] private Coroutine coroutine;

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;

    public void StartTypewriter()
    {
        StopAllCoroutines();

        if (tmpProText != null)
        {
            tmpProText.text = "";
            coroutine = StartCoroutine(TypeWriterTMP());
        }
        else
        {
            Debug.LogError("TMP_Text component is not assigned.");
        }
    }

    //public IEnumerator TextDeleteDelay()
    //{
    //    yield return new WaitForSeconds(2.5f);
    //    UI.Instance.NoRope.SetActive(false);
    //}
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator TypeWriterTMP()
    {
      
        typing = true;
        foreach (char c in writer)
        {
            tmpProText.text += c;
            yield return new WaitForSeconds(timeBtwChars);
            
        }
        typing = false;
      
    }
    //private IEnumerator TextDeleteDelay()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    UI.Instance.RunTXT.SetActive(false);
    //}

    public void StartTypewriterEffect(string message)
    {
            writer = message; // Set new message
            StartTypewriter(); // Start typing effect
        
    }
}

