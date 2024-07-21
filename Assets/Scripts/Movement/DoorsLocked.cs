using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DoorsLocked : MonoBehaviour
{
    public Animator door1;
    public GameObject openText;
    public GameObject KeyINV;
    public AudioSource doorSoundOpen;
    public AudioSource doorSoundClose;
    public AudioSource LockedSound;
    
    

    public LayerMask interactableLayer; // Add a LayerMask for raycasting

    private bool inReach;
    private bool doorOpen;
    public bool unlocked;
    public bool locked;
    public bool hasKey;

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
        if (KeyINV.activeInHierarchy)
        {
            locked = false;
            hasKey = true;
        }

        else
        {
            hasKey = false;
        }
        // Perform raycasting to check if the player is looking at the door
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 10f, interactableLayer))
        {
            Debug.Log("Raycast Hit: " + hit.collider.name); // Debug log to show what the raycast hits

            if (hit.collider.gameObject == gameObject) // Check if the raycast hit this door
            {
                Debug.Log("Looking at A Locked Door");

                // Check for interaction input
                if (inReach && Input.GetButtonDown("Interact") && doorOpen==false && hasKey == true)
                {
                    DoorOpens();
                    doorOpen = true;
                    
                    
                    
                }
                // Optional: You might want to keep the door open if looking at it and in reach
                else if (inReach && Input.GetButtonDown("Interact") && doorOpen==true)
                {
                    DoorCloses();
                    doorOpen = false;
                }

                if (inReach && Input.GetButtonDown("Interact") && hasKey == false && doorOpen == false)
                {
                    LockedSound.Play();
                }

            }
        }
       
    }

    void DoorOpens()
    {
        // Debug.Log("It Opens");
        door1.SetBool("Open", true);
        door1.SetBool("Closed", false);
        doorSoundOpen.Play();
    }

    void DoorCloses()
    {
        // Debug.Log("It Closes");
        door1.SetBool("Open", false);
        door1.SetBool("Closed", true);
        
        doorSoundClose.Play();
    }
    

}