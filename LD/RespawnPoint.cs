using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private int number = 1;
    [SerializeField] private VisualEffect vfx;
    [SerializeField] private AudioSource sound;
    [SerializeField] private Transform respawnPoint;
    private bool asPassed = false;
    [HideInInspector] public Vector3 respawnPos;
    

    private void Start()
    {
        respawnPos = Vector3.zero;
        respawnPos = respawnPoint.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 /*&& number > PlayerPrefs.GetInt("Checkpoint")*/)
        {
            sound.Play();
            vfx.SendEvent("CheckPointFirst");
            asPassed = true;

            if (other.gameObject.TryGetComponent<Player>(out Player play))
                play.life = play.maxLife;
        }
    }
}
