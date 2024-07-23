using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public GameObject Paperr;
    public GameObject interactText;
    public AudioSource PickUpPaper;

    private bool inReach;

    public LayerMask interactableLayer;


    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            interactText.SetActive(false);
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

            if (hit.collider.gameObject == gameObject)
            {
                inReach = true;
                if (inReach && Input.GetButtonDown("Interact") && Paperr.activeInHierarchy == true)
                {
                    Debug.Log("Picked up Key");
                    Paperr.SetActive(false);
                    interactText.SetActive(false);
                    PickUpPaper.Play();
                }
            }
        }
    }
}
