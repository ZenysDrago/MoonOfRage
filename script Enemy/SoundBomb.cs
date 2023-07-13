using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBomb : MonoBehaviour
{

    [Header("Audio Sources")]
    public AudioSource soundFireUp;
    public AudioSource soundExplosion;
    public AudioSource soundBigHit;

    public void LaunchFireUp()
    {
        if (!soundFireUp.isPlaying)
            soundFireUp.Play();
    }

    public void LaunchExplose()
    {
        soundExplosion.Play();
    }

    public void LaunchBigHit()
    {
        if (!soundBigHit.isPlaying)
            soundBigHit.Play();
    }
}
