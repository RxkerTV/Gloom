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
    public GameObject interactText;
    public GameObject PauseMenu;
    public List<ItemSlot> itemSlotList = new List<ItemSlot>();

    

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
