//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class AttatchRope : SingletonMonoBehaviour<AttatchRope>
//{

//    public bool RopeAttatched = false;
//    public
//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Reach"))
//        {
//            inReach = true;
//            UI.Instance.interactText.SetActive(true);
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Reach"))
//        {
//            inReach = false;
//            UI.Instance.interactText.SetActive(false);
//        }
//    }
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        RaycastHit hit;
//        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

//        if (Physics.Raycast(ray, out hit, 2.5f, PlayerCam.Instance.interactableLayer))
//        {
//            if (hit.collider.gameObject == gameObject)
//            {
//                PlayerCam.Instance.inReach = true;
//                // Check for interaction input
//                if (Input.GetButtonDown("Interact"))
//                {
//                    RopeAttatched = true;

//                }
//            }
//        }
//    }
//    void RocksTrig()
//    {

//    }
//}
