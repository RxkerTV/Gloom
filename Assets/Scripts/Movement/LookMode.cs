using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Gives us acsess to post processing 
using UnityEngine.Rendering.PostProcessing;

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



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(N))
        {
            // nightVisionOn = !nightVisionOn;  // Toggle the state
            if (nightVisionOn == false)
            {
                vol.profile = nightVision;
                nightVisionOverlay.SetActive(true);
                nightVisionOn = true;
                NightVisionOff();
                Debug.Log("Onnow");
            }
            else if (nightVisionOn == true)
            {
                vol.profile = standard;
                nightVisionOverlay.SetActive(false);
                cam.fieldOfView = 60;
                nightVisionOn = false;
                Debug.Log("Offnow");

            }
        }

        if (Input.GetKeyDown(F))
        {

            if (flashLightOn == false)
            {
                flashlight.enabled = true;
                flashLightOn = true;
                FlashLightSwitchOff();
                Debug.Log("FOnnow");
            }

            else if (flashLightOn == true)
            {
                flashlight.enabled = false;
                flashLightOn = false;
                Debug.Log("FOffnow");
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
         flashlight.enabled = false;
            flashLightOn = false;
        }

        }




