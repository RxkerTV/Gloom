using TMPro;
using UnityEngine;

public class Pickupflashlight : MonoBehaviour
{
    public GameObject flashlight;

    public LayerMask interactableLayer; // Add a LayerMask for raycasting
    private bool inReach;


    void Start()
    {
        inReach = false;
        UI.Instance.interactText.SetActive(false); // Ensure the text is hidden at the start
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            UI.Instance.interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            UI.Instance.interactText.SetActive(false);
        }
    }

    void Update()
    {
        // Perform raycasting to check if the player is looking at the door
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 2.5f, interactableLayer))
        {
            if (hit.collider.gameObject == gameObject) // Check if the raycast hit this door
            {

                // Check for interaction input
                if (inReach && Input.GetButtonDown("Interact"))
                { 
                    flashlight.SetActive(false);
                    LookMode.Instance.flashlightinInv = true;
                    UI.Instance.interactText.SetActive(false);
                }
            }
        }

    }
}