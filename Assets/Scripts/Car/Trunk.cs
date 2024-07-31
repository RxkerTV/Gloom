using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    public Animator Car_;
    public LayerMask interactableLayer; // Add a LayerMask for raycasting
    private bool inReach;
    public bool trunkOpen = false;

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
                    if (trunkOpen == false)
                    {
                        trunkOpens();
                        trunkOpen = true;
                        return;
                    }

                    else if (trunkOpen == true)
                    {
                        trunkCloses();
                        trunkOpen = false;
                        return;
                    }
                }
    }
        }
    }

    void trunkOpens()
    {
        Car_.SetBool("Open", true);
        Car_.SetBool("Closed", false);
    }

    void trunkCloses()
    {
        Car_.SetBool("Open", false);
        Car_.SetBool("Closed", true);
    }
}
