using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Gives us access to post processing 
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements;

public class LookMode : SingletonMonoBehaviour<LookMode>
{
    public PostProcessVolume vol;
    [Header("PostProcessVolumes")]
    public PostProcessProfile standard;
    public PostProcessProfile nightVision;
    public PostProcessProfile inventory;
    [Header("Keybinds")]
    public KeyCode N = KeyCode.N;
    public KeyCode F = KeyCode.F;
    public KeyCode Tab = KeyCode.Tab;
    public GameObject nightVisionOverlay;
    public Light flashlight;
    public bool nightVisionOn = false;
    private Camera cam;
    public bool flashLightOn = false;
    public bool PauseMenuOn = false;
    public bool flashlightinInv = false;


    // Start is called before the first frame update
    void Start()
    {

        vol = GetComponent<PostProcessVolume>();
        flashlight = GameObject.Find("flashlight").GetComponent<Light>();
        flashlight.enabled = false;
        nightVisionOverlay.SetActive(false);
        vol.profile = standard;
        cam = GameObject.Find("PlayerCam").GetComponent<Camera>();
        UI.Instance.InventoryMenu.SetActive(false);
        PauseMenuOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && PlayerCam.Instance.InventoryOn == false && PauseMenuOn == false)
        {
            nightVisionOn = !nightVisionOn;
            ToggleNightVision(nightVisionOn);
        }

        if (Input.GetKeyDown(KeyCode.F) && PlayerCam.Instance.InventoryOn == false && PauseMenuOn == false && flashlightinInv==true)
        {
            flashLightOn = !flashLightOn;
            ToggleFlashlight(flashLightOn);
        }

        if (Input.GetKeyDown(KeyCode.Tab) && PauseMenuOn == false)
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (PauseMenuOn == false)
            {
               
                vol.profile = inventory;
                PauseMenuOn = true;
                flashlight.enabled = false;
                nightVisionOverlay.SetActive(false);
                UI.Instance.PauseMenu.SetActive(true);
                UI.Instance.PauseOptions.SetActive(true);
                UI.Instance.InventoryMenu.SetActive(false);
            }

        }
    }
    private void ToggleNightVision(bool state)
    {
        if (state)
        {
            vol.profile = nightVision;
            nightVisionOverlay.SetActive(true);

        }
        else
        {
            vol.profile = standard;
            nightVisionOverlay.SetActive(false);
            cam.fieldOfView = 60;

        }
    }

    private void ToggleFlashlight(bool state)
    {
        flashlight.enabled = state;

    }


    private void ToggleInventory()
    {
        PlayerCam.Instance.InventoryOn = !PlayerCam.Instance.InventoryOn;

        if (PlayerCam.Instance.InventoryOn)
        {
            vol.profile = inventory;
            flashlight.enabled = false;
            nightVisionOverlay.SetActive(false);
            UI.Instance.InventoryMenu.SetActive(true);
        }
        else
        {
            vol.profile = standard;
            UI.Instance.InventoryMenu.SetActive(false);
        }
    }
}
//    private void TogglePauseMenu()
//    {
//        if(PauseMenuOn == false && GetKeyDown(KeyCode.Q))
//        {
//            vol.profile = inventory;
//            PauseMenuOn = true;
//            flashlight.enabled = false;
//            nightVisionOverlay.SetActive(false);
//            UI.Instance.PauseMenu.SetActive(true);
//        }
//        else if (PauseMenuOn == true)
//        {
//            vol.profile = standard;
//            PauseMenuOn = false;
//            UI.Instance.PauseMenu.SetActive(false);
//        }
//    }
//}

