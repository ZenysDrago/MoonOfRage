using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamHitBox : MonoBehaviour
{

    [SerializeField] private float damage = 50;
    [SerializeField] private float forceSlam = 20;
    private Player posPlayer;

    private void Start()
    {
        posPlayer = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Vector3 VecEnemy = other.transform.position - posPlayer.transform.position;
            Enemy en = other.gameObject.GetComponent<Enemy>();
            //en.rbody.AddForce(VecEnemy * forceSlam, ForceMode.Impulse);
            en.GetHit(damage);
        }
    }
}
