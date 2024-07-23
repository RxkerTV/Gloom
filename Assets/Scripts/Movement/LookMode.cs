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
    }
    #region Update
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && PlayerCam.Instance.inventoryOn == false)
        {
            nightVisionOn = !nightVisionOn;
            ToggleNightVision(nightVisionOn);
        }

        if (Input.GetKeyDown(KeyCode.F) && PlayerCam.Instance.inventoryOn == false)
        {
            flashLightOn = !flashLightOn;
            ToggleFlashlight(flashLightOn);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (PlayerCam.Instance.inventoryOn==false)
            {
                PlayerCam.Instance.inventoryOn = true;
                vol.profile = inventory;
                flashlight.enabled = false;
                nightVisionOverlay.SetActive(false);

            }
            else if (PlayerCam.Instance.inventoryOn == true)
            {
                PlayerCam.Instance.inventoryOn = false;
                vol.profile = standard;
            }
        }
    }
    #endregion
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
        vol.profile = inventory;
        
    }
}
