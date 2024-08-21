using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Ropescript : SingletonMonoBehaviour<Ropescript>
{
    public GameObject Rope;
    public bool HasRope;
    private bool inReach;

    private void Start()
    {
        HasRope = false;
        inReach = false;
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

        if (Physics.Raycast(ray, out hit, 2.5f, PlayerCam.Instance.interactableLayer))
        {
            if (hit.collider.gameObject == gameObject) // Check if the raycast hit this door
            {

                // Check for interaction input
                if (inReach && Input.GetButtonDown("Interact"))
                {
                    HasRope = true;
                    Rope.SetActive(false);
                    LookMode.Instance.flashlightinInv = true;
                    UI.Instance.interactText.SetActive(false);
                }
            }
        }

    }
}