using System.Collections;
using System.Collections.Generic;
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
    private bool inReach;
    public Transform inventoryParent; // The parent object of the inventory item slots (GLG)
    private void Start()
    {
        InventoryOn = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            UI.Instance.interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            UI.Instance.interactText.SetActive(false);
        }

    }
    private void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        if (InventoryOn == false)
        {
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

            // raycast logic
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 2.5f, Color.red); // Adjust the length (10f) and color (Color.red) as needed

            if (Physics.Raycast(ray, out hit, 2.5f, interactableLayer))
            {
                { 
                inReach = true;
               
                }

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (InventoryOn == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }
}
