using Unity.VisualScripting;
using UnityEngine;

public class RockEntrance : MonoBehaviour
{
    public Animator rock;
    public AudioSource RocksfallingSound;
    public bool rockshaventfell = true;
    public GameObject outsidelight;
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
        if (other.CompareTag("Player") && rockshaventfell)
        {
            RocksTrig();
            rockshaventfell = false;
            outsidelight.SetActive(false);
        }
    }
    void RocksTrig()
    {
        rock.SetTrigger("Entered");
        Debug.Log("RocksFell");
        RocksfallingSound.Play();
    }
}
