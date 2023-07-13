using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSound : MonoBehaviour
{
    public AudioSource soundHit;
    public AudioSource soundRolling;
    public AudioSource soundRollingSnow;
    public AudioSource soundControlled;
    public GroundType ground;

    public void LaunchImpact(float volumeImpact)
    {
        soundHit.volume = volumeImpact;
        soundHit.Play();
    }

    public void LaunchRoll()
    {
        switch(ground)
        {
            case GroundType.SNOW:
                if (!soundRollingSnow.isPlaying)
                    soundRollingSnow.Play();
                break;
            default:
                if (!soundRolling.isPlaying)
                    soundRolling.Play();
                break;
        }
    }

    public void StopRoll()
    {
        switch (ground)
        {
            case GroundType.SNOW:
                if (soundRollingSnow.isPlaying)
                    soundRollingSnow.Stop();
                break;
            default:
                if (soundRolling.isPlaying)
                    soundRolling.Stop();
                break;
        }
    }

    public void LaunchControll()
    {
        if (!soundControlled.isPlaying)
            soundControlled.Play();
    }

    public void StopControll()
    {
        if (soundControlled.isPlaying)
            soundControlled.Stop();
    }
}
