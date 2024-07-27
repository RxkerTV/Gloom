using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public GameObject Paperr;
    public AudioSource PickUpPaper;
    private bool inReach;
    //public NoteData noteData;
    public LayerMask interactableLayer;
    public Transform inventoryParent; // The parent object of the inventory item slots (GLG)
    public Transform playerTransform; // Reference to the player's transform

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

    private void Update()
    {
        if (playerTransform == null)
        {
            return;
        }


        {
            // Create the ray from the player's position and forward direction
            Ray ray = new Ray(playerTransform.position, playerTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2.5f, interactableLayer))
            {
                // Check if the interact button is pressed
                if (Input.GetButtonDown("Interact"))
                {
                    Note note = hit.collider.GetComponent<Note>();
                    if (note != null && note.noteData != null)
                    {
                        ItemSlot[] slots = inventoryParent.GetComponentsInChildren<ItemSlot>();
                        foreach (ItemSlot slot in slots)
                        {
                            if (slot.empty)
                            {
                                slot.SetItem(note.noteData);
                                UI.Instance.interactText.SetActive(false);
                                PickUpPaper.Play();
                                Destroy(hit.collider.gameObject);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}