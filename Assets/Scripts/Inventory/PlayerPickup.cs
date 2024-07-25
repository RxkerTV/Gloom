//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static UnityEditor.Progress;

//public class PlayerPickup : MonoBehaviour
//{
//    public Transform inventoryParent; // The parent object of the inventory item slots (GLG)

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Item")) // Assuming your items have the "Item" tag
//        {
//            NoteData note = other.GetComponent<NoteData>();
//            {
//                ItemSlot[] slots = inventoryParent.GetComponentsInChildren<ItemSlot>();

//                foreach (ItemSlot slot in slots)
//                {
//                    if (slot.empty)
//                    {
//                        slot.SetItem(note);
//                        Destroy(other.gameObject); // Remove the item from the scene

//                        return;
//                    }
//                }
//            }
//        }
//    }
//}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerPickup : MonoBehaviour
//{
//    public Transform inventoryParent; // The parent object of the inventory item slots (GLG)
//    public float interactionDistance = 3f; // The distance the raycast should reach
//    public LayerMask interactableLayer; // The layer mask for interactable objects
//    public KeyCode interactKey = KeyCode.E; // The key to press for interaction

//    private void Update()
//    {
//        // Perform the raycast
//        RaycastHit hit;
//        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactableLayer))
//        {
//            // Check if the hit object has the "Item" tag
//            if (hit.collider.CompareTag("Item"))
//            {
//                // Check if the interact key is pressed
//                if (Input.GetKeyDown(KeyCode.E))
//                {
//                    NoteData note = hit.collider.GetComponent<NoteData>();
//                    if (note != null)
//                    {
//                        ItemSlot[] slots = inventoryParent.GetComponentsInChildren<ItemSlot>();

//                        foreach (ItemSlot slot in slots)
//                        {
//                            if (slot.empty)
//                            {

//                                slot.SetItem(note);
//                                Destroy(hit.collider.gameObject); // Remove the item from the scene
//                                return;
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
