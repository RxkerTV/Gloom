using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarDoor : MonoBehaviour
{
    public Animator cardoor_;
    public LayerMask interactableLayer; // Add a LayerMask for raycasting
    private bool inReach;
    public bool doorOpen = false;

    // Start is called before the first frame update
    void Start()
    {

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


    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 2.5f, interactableLayer))
        {
            if (hit.collider.gameObject == gameObject) // Check if the raycast hit this door
            {
                inReach = true;
                if (inReach && Input.GetButtonDown("Interact"))
                {
                    if (doorOpen == false)
                    {
                        carDoorOpens();
                        doorOpen = true;
                        return;
                    }

                    if (doorOpen == true)
                    {
                        carDoorCloses();
                        doorOpen = false;
                        return;
                    }
                }
            }
        }
    }

    void carDoorOpens()
    {
        cardoor_.SetBool("Open", true);
        cardoor_.SetBool("Closed", false);
    }

    void carDoorCloses()
    {
        cardoor_.SetBool("Open", false);
        cardoor_.SetBool("Closed", true);
    }
}
