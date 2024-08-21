using UnityEngine;

public class Dropoff1 : MonoBehaviour
{
    public GameObject DropOffBlock;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Ropescript.Instance.HasRope == false)
        {
            UI.Instance.NoRope.SetActive(true);
        }

        if (other.CompareTag("Player") && Ropescript.Instance.HasRope == true)
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        UI.Instance.NoRope.SetActive(false);
    }
}
