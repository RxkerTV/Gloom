using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayASound : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioClip MusicClip;
    public AudioSource AmbientSource;
    public AudioClip AmbientClip;

    // Start is called before the first frame update
    void Start()
    {
        PlaySound(MusicSource, MusicClip);
        PlaySound(AmbientSource, AmbientClip);
    }

    void PlaySound(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();

    }
}
