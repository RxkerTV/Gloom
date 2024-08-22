using Unity.VisualScripting;
using UnityEngine;

public class Spiked : MonoBehaviour
{
    public Animator Spike;
    public AudioSource RocksfallingSound;
    public bool Spikehasntfallen = true;
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
        if (other.CompareTag("Player") && Spikehasntfallen)
        {
            RocksTrig();
            Spikehasntfallen = false;
        }
    }
    void RocksTrig()
    {
        Spike.SetTrigger("SpikeTrigger");
        Debug.Log("SpikeFell");
        RocksfallingSound.Play();
    }
}
