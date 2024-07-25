using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerPickup : MonoBehaviour
{
    public Transform inventoryParent; // The parent object of the inventory item slots (GLG)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item")) // Assuming your items have the "Item" tag
        {
            {
                ItemSlot[] slots = inventoryParent.GetComponentsInChildren<ItemSlot>();

                foreach (ItemSlot slot in slots)
                {
                    if (slot.empty)
                    {
                        slot.SetItem(Paper);
                        Destroy(other.gameObject); // Remove the item from the scene

                        return;
                    }
                }