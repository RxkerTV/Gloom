using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Gives us acsess to post processing 
using UnityEngine.Rendering.PostProcessing;

public class LookMode : MonoBehaviour
{
    private PostProcessVolume vol;
    public PostProcessProfile standard;
    public PostProcessProfile nightVision;
    public PostProcessProfile inventory;
    public GameObject nightVisionOverlay;
    public GameObject flashLightOverlay;
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
        flashLightOverlay.SetActive(false);
        vol.profile = standard;
        cam = GameObject.Find("PlayerCam").GetComponent<Camera>();

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            // nightVisionOn = !nightVisionOn;  // Toggle the state
            if (nightVisionOn == false)
            {
                vol.profile = nightVision;
                nightVisionOverlay.SetActive(true);
                nightVisionOn = true;
                NightVisionOff();
            }
            else if (nightVisionOn == true)
            {
                vol.profile = standard;
                nightVisionOverlay.SetActive(false);
                cam.fieldOfView = 60;
                nightVisionOn = false;

            }
        }

        if (Input.GetKeyDown(KeyCode.F))

        {

            if (flashLightOn == false)
            {
                flashLightOverlay.SetActive(true);
                flashlight.enabled = true;
                flashLightOn = true;
                FlashLightSwitchOff();
            }

            else if (flashLightOn == true)
            {
                flashLightOverlay.SetActive(false);
                flashlight.enabled = false;
                flashLightOverlay.GetComponent<FlashLightScript>().StopDrain();
                flashLightOn = false;

            }

        }
    
        if (nightVisionOn == true)
        {
            NightVisionOff();
        }

        if (flashLightOn == true)
        {
            FlashLightSwitchOff();
        }
    }
        private void NightVisionOff()
        {
            {
            vol.profile = standard;
            nightVisionOverlay.SetActive(false);
            cam.fieldOfView = 60;
            nightVisionOn = false;
        }
        }
        private void FlashLightSwitchOff()
        {
            if (flashLightOverlay.GetComponent<FlashLightScript>().batteryPower <= 0)
            {
            flashLightOverlay.SetActive(false);
            flashlight.enabled = false;
            flashLightOverlay.GetComponent<FlashLightScript>().StopDrain();
            flashLightOn = false;
        }
        }
    }




