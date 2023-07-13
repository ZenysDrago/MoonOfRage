using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    private Transform posPlayer;
    public float forceShockWave = 5;
    public float forceShockWaveToMoon = 25f;
    public float damage = 5f;
    // Start is called before the first frame update
    void Start()
    {
        posPlayer = GameObject.FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<Enemy>().GetHit(damage);

            Vector3 VecEnemy = other.transform.position - posPlayer.position;
            //other.gameObject.GetComponent<Enemy>().rbody.AddForce(VecEnemy * forceShockWave, ForceMode.Impulse);
            other.gameObject.GetComponent<Enemy>().rbody.AddForce(Vector3.up * forceShockWave, ForceMode.Impulse);
            
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 9)
        {
            Vector3 VecMoon = other.transform.position - posPlayer.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(VecMoon.x, 0, VecMoon.z) * forceShockWaveToMoon, ForceMode.Impulse);
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == 13)
        {
            Vector3 VecBox = other.transform.position - posPlayer.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(VecBox.x, 0, VecBox.z) * forceShockWave, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
