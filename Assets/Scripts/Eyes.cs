using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public LayerMask interactableLayer;
    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0; // Keep the y-axis unchanged
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }


        // Perform raycasting to check if the player is looking at the door
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 10f, interactableLayer))
        {
            if (hit.collider.gameObject == gameObject) // Check if the raycast hit this door
            {
                Destroy(this.gameObject);
            }
        }
    }
}
