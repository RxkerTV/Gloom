using Unity.VisualScripting;
using UnityEngine;

public class RockEntrence : MonoBehaviour
{
    private bool inCol = false;
    public Animator rock;
    public AudioSource RocksfallingSound;
    public bool rockshaventfell = true;
    public GameObject outsidelight;
    // Start is called before the first frame update
    void Start()
    {
        RocksfallingSound.mute = true;
        Debug.Log("RocksfallingSound is muted on start: " + RocksfallingSound.mute);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && rockshaventfell)
        {
            RocksfallingSound.mute = false;
            inCol = true;
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
