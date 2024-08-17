using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    public AudioSource TriggerSource;
    public AudioClip TriggerClip;
    void Start()
    {
        TriggerSource.clip = TriggerClip;
    }

    private void OnTriggerEnter(Collider player)
    {
        TriggerSource.Play();
        gameObject.SetActive(false);
    }
}
