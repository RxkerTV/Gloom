using UnityEngine;
using TMPro;

public class PadLock : SingletonMonoBehaviour<PadLock>
{
    public string correctCode = "1234"; // The correct code for the lock
    public TMP_Text displayText; // Reference to the TextMeshPro display
    private string currentCode = ""; // The code the player is currently entering

    public void AddDigit(string digit)
    {
        if (currentCode.Length < 4)
        {
            currentCode += digit;
            UpdateDisplay();
        }
    }

    public void CheckCode()
    {
        if (currentCode == correctCode)
        {
            Unlock();
        }
        else
        {
            currentCode = ""; // Reset the code if it's wrong
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        displayText.text = currentCode.PadRight(4, '_'); // Shows the current code or underscores
    }

    private void Unlock()
    {
        Debug.Log("Lock Opened!");
        //Player Unlock noise.
        
       // PlayerCam.Instance.LockerUnlocked = true;
    }
}