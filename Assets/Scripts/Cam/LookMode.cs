using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Gives us access to post processing 
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements;

public class LookMode : MonoBehaviour
{
    private PostProcessVolume vol;
    [Header("PostProcessVolumes")]
    public PostProcessProfile standard;
    public PostProcessProfile nightVision;
    public PostProcessProfile inventory;
    [Header("Keybinds")]
    public KeyCode N = KeyCode.N;
    public KeyCode F = KeyCode.F;
    public KeyCode Tab = KeyCode.Tab;
    public GameObject nightVisionOverlay;
    private Light flashlight;
    private bool nightVisionOn = false;
    private Camera cam;
    private bool flashLightOn = false;

    

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && PlayerCam.Instance.InventoryOn == false)
        {
            nightVisionOn = !nightVisionOn;
            ToggleNightVision(nightVisionOn);
        }

        if (Input.GetKeyDown(KeyCode.F) && PlayerCam.Instance.InventoryOn == false)
        {
            flashLightOn = !flashLightOn;
            ToggleFlashlight(flashLightOn);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleInventory();
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

