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
            Debug.LogError("Player transform is not assigned.");
            return;
        }

        // Create the ray from the player's position and forward direction
        Ray ray = new Ray(playerTransform.position, playerTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2.5f, interactableLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction * 2.5f, Color.red, 1f);
            Debug.Log($"Raycast hit: {hit.collider.name}");

            Note note = hit.collider.GetComponent<Note>();
            if (note == null)
            {
                Debug.Log("No Note component found on hit object.");
            }
            else
            {
                if (note.noteData == null)
                {
                    Debug.Log("NoteData is not assigned.");
                }
                else
                {
                    Debug.Log($"NoteData found: {note.noteData.name}");
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