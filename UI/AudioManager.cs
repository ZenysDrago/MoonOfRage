using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        }
        else
        {
            PlayerPrefs.SetFloat("MasterVol", 0);
            mixer.SetFloat("MasterVol", 0);
        }
    }
}
