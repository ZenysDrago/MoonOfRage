using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnemy : MonoBehaviour
{
    private int currentAttack = 0;
    private int currentHit = 0;


    [Header("Audio Sources")]
    public AudioSource soundMovement;
    public AudioSource[] soundAttack;
    public AudioSource[] soundHit;
    public AudioSource soundBigHit;

    public void LaunchAttack()
    {
        if (!soundAttack[currentAttack].isPlaying)
        {
            currentAttack = Random.Range(0, soundAttack.Length - 1);
            soundAttack[currentAttack].Play();
        }
    }

    public void LaunchHit()
    {
        if (!soundHit[currentHit].isPlaying)
        {
            currentHit = Random.Range(0, soundHit.Length - 1);
            soundHit[currentHit].Play();
        }
    }

    public void LaunchWalk()
    {
        if (!soundMovement.isPlaying)
            soundMovement.Play();
    }

    public void StopWalk()
    {
        if (soundMovement.isPlaying)
            soundMovement.Stop();
    }

    public void LaunchBigHit()
    {
        if(!soundBigHit.isPlaying)
            soundBigHit.Play();
    }

}
