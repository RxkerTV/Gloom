using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterUI : MonoBehaviour
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

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator TypeWriterTMP()
    {
        //tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        //yield return new WaitForSeconds(delayBeforeStart);
        typing = true;
        foreach (char c in writer)
        {
            //if (tmpProText.text.Length > 0)
            //{
            //    tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
            //}
            tmpProText.text += c;
            //tmpProText.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
            
        }
        typing = false;
        //if (leadingChar != "")
        //{
        //    tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
        //}
    }
}
