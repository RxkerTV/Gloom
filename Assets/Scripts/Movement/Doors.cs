﻿using UnityEngine;

public class Doors : MonoBehaviour
{
    public Animator door;
    public GameObject openText;
    public AudioSource doorSound;

    public LayerMask interactableLayer; // Add a LayerMask for raycasting

    private bool inReach;

    void Start()
    {
        inReach = false;
        openText.SetActive(false); // Ensure the text is hidden at the start
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            Debug.Log("inreach is true");
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            openText.SetActive(false);
        }
    }

    void Update()
    {
        // Perform raycasting to check if the player is looking at the door
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 10f, interactableLayer))
        {
            Debug.Log("Raycast Hit: " + hit.collider.name); // Debug log to show what the raycast hits

            if (hit.collider.gameObject == gameObject) // Check if the raycast hit this door
            {
                Debug.Log("Looking at Door");

                // Check for interaction input
                if (inReach && Input.GetButtonDown("Interact"))
                {
                    DoorOpens();
                }
                // Optional: You might want to keep the door open if looking at it and in reach
                else if (inReach)
                {
                    // Optional: Add code to keep the door open or handle other cases if needed
                }
            }
        }
        // Handle door closing when not looking at it and interact button is pressed
        else if (inReach && Input.GetButtonDown("Interact"))
        {
            DoorCloses();
        }
    }

    void DoorOpens()
    {
        // Debug.Log("It Opens");
        door.SetBool("Open", true);
        door.SetBool("Closed", false);
        doorSound.Play();
    }

    void DoorCloses()
    {
        // Debug.Log("It Closes");
        door.SetBool("Open", false);
        door.SetBool("Closed", true);
    }
}