//using UnityEngine;

//public static class RaycastHelper
//{
//    private static LayerMask interactableLayer = LayerMask.GetMask("Interactable");

//    public static bool PerformRaycast(out RaycastHit hit, float distance)
//    {
//        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
//        return Physics.Raycast(ray, out hit, distance, interactableLayer);
//    }
//}