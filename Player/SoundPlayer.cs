using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType
{
    CLASSIC,
    GRASS,
    SNOW,
}


public class SoundPlayer : MonoBehaviour
{
    private int currentMovementSound = 0;
    private int currentMovementSoundGrass = 0;
    private int currentMovementSoundSnow = 0;

    private int currentAttackSound = 0;
    private int currentMissedAttackSound = 0;

    [HideInInspector] public GroundType ground;

    [Header("Audio Sources")]
    public AudioSource[] soundMovement = new AudioSource[9];
    public AudioSource[] soundMovementGrass = new AudioSource[17];
    public AudioSource[] soundMovementSnow = new AudioSource[12];

    public AudioSource[] soundAttacks = new AudioSource[12];
    public AudioSource[] soundMissedAttacks = new AudioSource[4];

    public AudioSource soundLinkStart;
    public AudioSource soundLinkEnd;
    public AudioSource soundUppercut;
    public AudioSource soundHeavyAttack;
    public AudioSource soundShockWave;

    private void Update()
    {
        
    }

    // Start Sound
    public void SoundCreateLink()
    {
        if(!soundLinkStart.isPlaying)
        {
            soundLinkEnd.Stop();
            soundLinkStart.Play();
        }
    }

    public void LaunchStep()
    {
        switch(ground)
        {
            case GroundType.CLASSIC:
                if (!soundMovement[currentMovementSound].isPlaying)
                {
                    currentMovementSound = Random.Range(0, 9 /* nb movement */ - 1);
                    soundMovement[currentMovementSound].Play();
                }
                break;
            case GroundType.SNOW:
                if (!soundMovementSnow[currentMovementSoundSnow].isPlaying)
                {
                    currentMovementSoundSnow = Random.Range(0, 12 /* nb movement  snow */ - 1);
                    soundMovementSnow[currentMovementSoundSnow].Play();
                }
                break;
            case GroundType.GRASS:
                if (!soundMovementGrass[currentMovementSoundGrass].isPlaying)
                {
                    currentMovementSoundGrass = Random.Range(0, 17 /* nb movement grass  */ - 1);
                    soundMovementGrass[currentMovementSoundGrass].Play();
                }
                break;
            default:
                break;
        }

    }

    public void LaunchAttack()
    {
        if (!soundAttacks[currentAttackSound].isPlaying)
        {
            currentAttackSound = Random.Range(0, 12 /* nb  attack */ - 1);
            soundAttacks[currentAttackSound].Play();   
        }
    }
    public void LaunchMissedAttack()
    {
        if (!soundMissedAttacks[currentMissedAttackSound].isPlaying)
        {
            currentMissedAttackSound = Random.Range(0, 4 /* nb missed attack */ - 1);
            soundMissedAttacks[currentMissedAttackSound].Play();
        }
    }

    public void LaunchUppercut()
    {
        if (!soundUppercut.isPlaying)
            soundUppercut.Play();
    }

    public void LaunchHeavyAttack()
    {
        if (!soundHeavyAttack.isPlaying)
            soundHeavyAttack.Play();
    }

    public void LaunchShockWave()
    {
        soundShockWave.Play();
    }

    //End Sound
    public void StopMovement()
    {
        soundMovement[currentMovementSound].Stop();
    }

    public void SoundDestroyLink()
    {
        if (!soundLinkEnd.isPlaying)
        {
            soundLinkStart.Stop();
            soundLinkEnd.Play();
        }
    }
}
