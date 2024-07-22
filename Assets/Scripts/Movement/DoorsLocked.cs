using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DoorsLocked : SingletonMonoBehaviour<DoorsLocked>
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
        locked = true;
        unlocked = false;
        inReach = false;
        doorOpen = false; // Initialize doorOpen
        hasKey = false; // Ensure hasKey is false at start
        openText.SetActive(false); // Ensure the text is hidden at the start
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
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

        if (Physics.Raycast(ray, out hit, 2.5f, interactableLayer))
        {
            Debug.Log("Raycast Hit: " + hit.collider.name); // Debug log to show what the raycast hits

            if (hit.collider.gameObject == gameObject) // Check if the raycast hit this door
            {
                // Check for interaction input
                if (inReach && Input.GetButtonDown("Interact") && !doorOpen && hasKey)
                {
                    DoorOpens();
                    doorOpen = true;
                    Debug.Log("Open");
                    locked = false;
                    unlocked = true;
                }
                else if (inReach && Input.GetButtonDown("Interact") && doorOpen && unlocked)
                {
                    DoorCloses();
                    doorOpen = false;
                    Debug.Log("close");
                }
                else if (inReach && Input.GetButtonDown("Interact") && !hasKey && !doorOpen)
                {
                    Debug.Log("Locked");
                    LockedSound.Play();
                }
            }
        }
    }

    void DoorOpens()
    {
        door1.SetBool("Open", true);
        door1.SetBool("Closed", false);
        doorSoundOpen.Play();
    }

    void DoorCloses()
    {
        door1.SetBool("Open", false);
        door1.SetBool("Closed", true);
        doorSoundClose.Play();
    }
}
