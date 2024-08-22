using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private float _footstepDistanceCounter;
    public float footstepFrequency = 1.0f;
    public AudioSource FootSource;
    public AudioSource LandSource;
    [Header("---AudioClips---")]
    [Header("Footsteps")]
    
    public AudioClip[] FootstepsSFX;
    [Header("Landing")]
    public AudioClip[] LandSFX;

    public Vector2 PitchRange = new Vector2(0.9f, 1.1f);
    public void PlayFootStep(Vector3 velocity)
    {
        if (_footstepDistanceCounter >= 1.0f/footstepFrequency)
        {
            _footstepDistanceCounter = 0f;
            
            AudioFunctionalities.PlayRandomClip(FootSource, FootstepsSFX, PitchRange.x, PitchRange.y);
        }

        _footstepDistanceCounter += velocity.magnitude * Time.deltaTime;
    }

    public void PlayLandingSound()
    {
        AudioFunctionalities.PlayRandomClip(LandSource, LandSFX, PitchRange.x, PitchRange.y);
    }
}
