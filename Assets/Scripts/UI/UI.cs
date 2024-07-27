using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : SingletonMonoBehaviour<UI>
{
    [Header("Inventory Panel")]
    public GameObject inventoryPanel;
    public GameObject itemSlots;
    public GameObject itemSlotPrefb;
    public GameObject InventoryMenu;
    public List<ItemSlot> itemSlotList = new List<ItemSlot>();
    public GameObject interactText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
