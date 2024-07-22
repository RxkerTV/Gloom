using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject Key;
    public GameObject interactText;

    private bool inReach;

    public LayerMask interactableLayer;



    // Start is called before the first frame update
    void Start()
    {
        inReach = false;
        interactText.SetActive(false);
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

    // Update is called once per frame
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
                if (inReach && Input.GetButtonDown("Interact") && Key.activeInHierarchy == true)
                {
                    Debug.Log("PickedupKey");
                    Key.SetActive(false);
                    DoorsLocked.Instance.hasKey = true;
                    interactText.SetActive(false);
                }
            }
        }

    }

}