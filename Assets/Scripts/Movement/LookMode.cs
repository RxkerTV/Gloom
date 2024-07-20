using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Gives us access to post processing 
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
            nightVisionOn = !nightVisionOn;
            ToggleNightVision(nightVisionOn);
        }

        if (Input.GetKeyDown(F))
        {
            flashLightOn = !flashLightOn;
            ToggleFlashlight(flashLightOn);
        }
    }

    private void ToggleNightVision(bool state)
    {
        if (state)
        {
            vol.profile = nightVision;
            nightVisionOverlay.SetActive(true);
            Debug.Log("Onnow");
        }
        else
        {
            vol.profile = standard;
            nightVisionOverlay.SetActive(false);
            cam.fieldOfView = 60;
            Debug.Log("Offnow");
        }
    }

    private void ToggleFlashlight(bool state)
    {
        flashlight.enabled = state;
        Debug.Log(state ? "FOnnow" : "FOffnow");
    }
}
