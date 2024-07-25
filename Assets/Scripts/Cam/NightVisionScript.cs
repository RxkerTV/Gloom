using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightVisionScript : MonoBehaviour
{
    //these allow you to put the item you want into the boxes on unity (public)
    //prive means it can only be access through script
    private Image zoomBar;
    private Image batteryChunks;
    private Camera cam;
    //public float batteryPower = 1.0f;
    //public float drainTime = 15;
    // Start is called before the first frame update
    void Start()
    {
        // this automaticly finds the item so you don't accidentally forget it
        zoomBar = GameObject.Find("ZoomBar").GetComponent<Image>();
        cam = GameObject.Find("PlayerCam").GetComponent<Camera>();

    }
    private void OnEnable()
    {
        if(zoomBar != null) 
        zoomBar.fillAmount = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(cam.fieldOfView > 10)
            {
                cam.fieldOfView -= 5;
                zoomBar.fillAmount = cam.fieldOfView / 100;
            }
           
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (cam.fieldOfView < 60)
            {
                cam.fieldOfView += 5;
                zoomBar.fillAmount = cam.fieldOfView / 100;
            }
        }
  
    }
}
