using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCam : SingletonMonoBehaviour<PlayerCam> 
{
    public float sensX;
    public float sensY;
    public LayerMask interactableLayer;
    public Transform orientation;
    public object FlashLight;
    float xRotation;
    float yRotation;
    public bool inventoryOn;

    private void Start()
    {
        inventoryOn = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        if (inventoryOn == false)
        {
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);


            // rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

            // raycast logic
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);

            Debug.DrawRay(ray.origin, ray.direction * 2.5f, Color.red); // Adjust the length (10f) and color (Color.red) as needed

            if (Physics.Raycast(ray, out hit, 2.5f, interactableLayer))
            {
                Debug.Log("Hit: " + hit.collider.name);

            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (inventoryOn == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
