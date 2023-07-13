using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSound : MonoBehaviour
{
    
    [SerializeField] GroundType groundLD;
    [SerializeField] PlayerController player;
    [SerializeField] AudioSource soundLevel;
    [SerializeField] AudioSource soundLevelArena;
    [SerializeField] AudioSource soundAmbientLevel;

    [Range(0f, 1f)] public float volumeMusicClassic;
    [Range(0f, 1f)] public float volumeMusicArena;
    [Range (0f,1f)] public float volumeAmbient;

    public float transitionTime;

    private bool isTransitionDone = true;
    private float timerTransition = 0f;
    private bool currentSoundClassic = true;

    private void Start()
    {
        soundLevel.volume = 1;
        soundLevelArena.volume = 0;

        soundLevel.loop = true;
        soundLevel.Play();
        soundLevelArena.loop = true;
        soundLevelArena.Play();

        soundAmbientLevel.Play();
        soundAmbientLevel.loop = true;

        player.sound.ground = groundLD;
        player.soundMoon.ground = groundLD;
    }

    private void Update()
    {
        //soundLevel.volume = volumeMusicClassic;
        //soundLevelArena.volume = volumeMusicArena;
        //soundAmbientLevel.volume = volumeAmbient;
        
        
        if (!isTransitionDone)
            TransitionSound();
    }

    private void TransitionSound()
    {
        if(!currentSoundClassic)
        {
            timerTransition += Time.deltaTime;

            soundLevel.volume      = Mathf.Lerp(0, 1, timerTransition / transitionTime);
            soundLevelArena.volume = Mathf.Lerp(1, 0, timerTransition / transitionTime);

            if(soundLevel.volume == 1)
            {
                isTransitionDone = true;
                currentSoundClassic = true;
                timerTransition = 0f;
            }

        }
        else
        {
            timerTransition += Time.deltaTime;

            soundLevelArena.volume = Mathf.Lerp(0, 1, timerTransition / transitionTime);
            soundLevel.volume      = Mathf.Lerp(1, 0, timerTransition / transitionTime);

            if (soundLevelArena.volume == 1)
            {
                isTransitionDone = true;
                currentSoundClassic = false;
                timerTransition = 0f;
            }
        }
    }


    public void LaunchTransition()
    {
        isTransitionDone = false;
    }

}
