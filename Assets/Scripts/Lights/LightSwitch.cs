using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Animator LightSwitch_;
    public GameObject lightSource;
    public bool lightbool = false;
    public LayerMask interactableLayer; // Add a LayerMask for raycasting
    private bool inReach;
    // Start is called before the first frame update
    void Start()
    {
        lightbool =false;
        lightSource.SetActive(false);
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
                    if (lightbool == false)
                    {
                        lightbool = true;
                        lightSource.SetActive(true);
                        Debug.Log("on");
                        return;
                    }
                    if (lightbool == true)
                    {
                        lightbool = false;
                        lightSource.SetActive(false);
                        Debug.Log("false");
                        return;
                    }
                }
            }
        }
    }
}
