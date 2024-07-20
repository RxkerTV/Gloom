using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableObject : MonoBehaviour, IPointerClickHandler
{
    public string interactionMessage = "Object Interacted!";

    public void OnPointerClick(PointerEventData eventData)
    {
        Interact();
    }

    private void Interact()
    {
        // Implement what happens when the interaction occurs
        Debug.Log(interactionMessage);
    }
}
