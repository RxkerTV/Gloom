using Unity.VisualScripting;
using UnityEngine;

public class RockEntrence : MonoBehaviour
{
    private bool inCol = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inCol = true;
            
        }
    }

}
