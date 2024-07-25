using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.U2D.ScriptablePacker;

public class Paper : MonoBehaviour
{
    public GameObject Paperr;
    public AudioSource PickUpPaper;
    private bool inReach;
    public NoteData noteData;
    public LayerMask interactableLayer;
    public Transform inventoryParent; // The parent object of the inventory item slots (GLG)

    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            UI.Instance.interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            UI.Instance.interactText.SetActive(false);
        }

    }
    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact") && Paperr.activeInHierarchy == true)
        {
            Paperr.SetActive(false);
            UI.Instance.interactText.SetActive(false);
            PickUpPaper.Play();

            NoteData note = GetComponent<NoteData>();
            {
                ItemSlot[] slots = inventoryParent.GetComponentsInChildren<ItemSlot>();

                foreach (ItemSlot slot in slots)
                {
                    if (slot.empty)
                    {
                        slot.SetItem(note);
                        Debug.Log($"{note.name} + {note.sentences}");
                        Destroy(gameObject); // Remove the item from the scene

                        return;
                    }
                }
            }
        }
    }
}
