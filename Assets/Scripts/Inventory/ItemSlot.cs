using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class ItemSlot : MonoBehaviour
{
    public Item item;
    public bool empty = true;
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Image itemImage;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }
    public void SetItem(Item newItem)
    {
        item = newItem;
        empty = false;
        itemImage.sprite = item.inventorySprite;
        itemImage.gameObject.SetActive(true); // Activate the image when an item is set
    }

    public void ClearSlot()
    {
        item = null;
        empty = true;
        itemImage.sprite = null;
        itemImage.gameObject.SetActive(false); // Deactivate the image when the slot is empty
    }

    public void Update()
    {
        if(empty)
        {

        }
    }
}
