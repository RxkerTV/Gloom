using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCam : SingletonMonoBehaviour<PlayerCam>
{
    public float sensX;
    public float sensY;
    public LayerMask interactableLayer;
    public Transform orientation;
    public object FlashLight;
    public Ray ray;
    float xRotation;
    float yRotation;
    public bool InventoryOn;
    public bool inReach;
    public bool HasRope;
    private bool doorOpen;
    public Transform inventoryParent; // The parent object of the inventory item slots (GLG)
    [Header("ObjectsForInteraction")]
    public GameObject flashlight;
    public GameObject Rope;
    //public GameObject Key;
    //public GameObject[] doors;

    //public Animator door;
    public AudioSource doorSoundOpen;
    public AudioSource doorSoundClose;
    private void Start()
    {
        UI.Instance.interactText.SetActive(false);
        InventoryOn = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Reach"))
    //    {
    //        inReach = true;
    //        UI.Instance.interactText.SetActive(true);
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Reach"))
    //    {
    //        inReach = false;
    //        UI.Instance.interactText.SetActive(false);
    //    }

    //}
    private void Update()
    {
        inReach = false;
        UI.Instance.interactText.SetActive(false);

        if (!InventoryOn && LookMode.Instance.PauseMenuOn == false)
        {
            // get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 2.5f, interactableLayer))
        {
            //foreach (GameObject doorObject in doors)
            //{
            //    if (hit.collider.gameObject == doorObject)
            //    {
            //        // Check for interaction input
            //        if (inReach && Input.GetButtonDown("Interact") && doorOpen == false)
            //        {
            //            DoorOpens();
            //            doorOpen = true;
            //            Debug.Log("Open");
            //        }
            //        else if (inReach && Input.GetButtonDown("Interact") && doorOpen == true)
            //        {
            //            DoorCloses();
            //            doorOpen = false;
            //            Debug.Log("Closed");
            //        }
            //        break; // Exit the loop once the door is found
            //    }
            //}

            if (hit.collider.gameObject == flashlight)
            {
                inReach = true;
                if (Input.GetButtonDown("Interact"))
                {
                    flashlight.SetActive(false);
                    LookMode.Instance.flashlightinInv = true;
                    UI.Instance.interactText.SetActive(false);
                }
            }

            if (hit.collider.gameObject == Rope)
            {

                inReach = true;
                if (Input.GetButtonDown("Interact"))
                {
                    HasRope = true;
                    Rope.SetActive(false);
                    UI.Instance.interactText.SetActive(false);
                }
            }
          
            //if (hit.collider.gameObject == Key)
            //{
            //    if (inReach && Input.GetButtonDown("Interact") && Key.activeInHierarchy == true)
            //    {
            //        Key.SetActive(false);
            //        DoorsLocked.Instance.hasKey = true; // Set hasKey to true when the key is picked up
            //        UI.Instance.interactText.SetActive(false);
            //    }

            
        }
        
    }
    //void DoorOpens()
    //{
    //    // Debug.Log("It Opens");
    //    door.SetBool("Open", true);
    //    door.SetBool("Closed", false);
    //    doorSoundOpen.Play();
    //}

    //void DoorCloses()
    //{
    //    // Debug.Log("It Closes");
    //    door.SetBool("Open", false);
    //    door.SetBool("Closed", true);
    //    doorSoundClose.Play();
    //}
}