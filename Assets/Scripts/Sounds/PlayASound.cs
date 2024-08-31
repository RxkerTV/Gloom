using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayASound : SingletonMonoBehaviour<PlayASound>
{
    public AudioSource MusicSource;
    public AudioClip MusicClip;
    public AudioSource AmbientSource;
    public AudioClip AmbientClip;
    public AudioSource FlashLightSource;
    public AudioClip FlashLightClick;

    // Start is called before the first frame update
    void Start()
    {
        PlaySound(MusicSource, MusicClip);
        PlaySound(AmbientSource, AmbientClip);
    }

    public void flashlightsound()
    {
        PlaySound(FlashLightSource, FlashLightClick);
    }

    public void PlaySound(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();

    }

}
