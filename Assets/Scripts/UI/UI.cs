using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : SingletonMonoBehaviour<UI>
{
    [Header("Inventory Panel")]
   // public GameObject inventoryPanel;
  //  public GameObject itemSlots;
  //  public GameObject itemSlotPrefb;
  //  public GameObject InventoryMenu;
    public GameObject interactText;
    [Header("PauseMENU----------")]
    public GameObject PauseMenu;
    public GameObject PauseOptions;
    [Header("Quit")]
    public GameObject Mainmenu;
    public GameObject Desktop;
    public GameObject Cancel;
    [Header("DropOff")]
    public GameObject NoRope;
    public GameObject UseRope;
    [Header("RAA")]
    public GameObject ToDo;
    public List<ItemSlot> itemSlotList = new List<ItemSlot>();

    public GameObject RunTXT;
    public GameObject FlashLightPickupPrompt;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
        PauseOptions.SetActive(false);
        Mainmenu.SetActive(false);
        Desktop.SetActive(false);
        Cancel.SetActive(false);
        NoRope.SetActive(false);
        UseRope.SetActive(false);
        RunTXT.SetActive(false);
        FlashLightPickupPrompt.SetActive(false);
    }

    void Update()
    {

    }
}
