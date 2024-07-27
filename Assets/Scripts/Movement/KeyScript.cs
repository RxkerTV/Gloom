using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject Key;

    private bool inReach;

    public LayerMask interactableLayer;

    // Start is called before the first frame update
    void Start()
    {
        inReach = false;
        UI.Instance.interactText.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        // Perform raycasting to check if the player is looking at the door
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 2.5f, interactableLayer))
        {
            if (hit.collider.gameObject == gameObject)
            {
                inReach = true;
                if (inReach && Input.GetButtonDown("Interact") && Key.activeInHierarchy == true)
                {
                    Key.SetActive(false);
                    DoorsLocked.Instance.hasKey = true; // Set hasKey to true when the key is picked up
                    UI.Instance.interactText.SetActive(false);
                }
            }
        }
    }
}

