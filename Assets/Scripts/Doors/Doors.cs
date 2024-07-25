using TMPro;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Animator door;
    public GameObject openText;
    public AudioSource doorSoundOpen;
    public AudioSource doorSoundClose;

    public LayerMask interactableLayer; // Add a LayerMask for raycasting

    private bool inReach;
    private bool doorOpen;
  
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
            if (hit.collider.gameObject == gameObject) // Check if the raycast hit this door
            { 

                // Check for interaction input
                if (inReach && Input.GetButtonDown("Interact") && doorOpen==false)
                {
                    DoorOpens();
                    doorOpen = true;
                    Debug.Log("Open");


                }
                // Optional: You might want to keep the door open if looking at it and in reach
                else if (inReach && Input.GetButtonDown("Interact") && doorOpen==true)
                {
                    DoorCloses();
                    doorOpen = false;
                    Debug.Log("Closed");
                }

            }
        }
       
    }

    void DoorOpens()
    {
        // Debug.Log("It Opens");
        door.SetBool("Open", true);
        door.SetBool("Closed", false);
        doorSoundOpen.Play();
    }

    void DoorCloses()
    {
        // Debug.Log("It Closes");
        door.SetBool("Open", false);
        door.SetBool("Closed", true);
        doorSoundClose.Play();
    }
}