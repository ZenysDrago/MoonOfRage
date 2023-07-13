using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heavyAttack : MonoBehaviour
{
    public float damage = 10f;
    public float forceHeavyAttack = 15000f;
    public float forceHeavyAttackToMoon = 25f;

    private bool attackSuceed = false;
    private Transform posPlayer;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        posPlayer = player.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 8)
        {
            attackSuceed = true;
            player.sound.LaunchHeavyAttack();
            other.gameObject.GetComponent<Enemy>().GetHit(damage);
            Vector3 VecEnemy = other.transform.position - posPlayer.position;
            other.gameObject.GetComponent<Enemy>().rbody.velocity = Vector3.zero;
            //other.gameObject.GetComponent<Enemy>().rbody.AddForce(new Vector3(VecEnemy.x, 0, VecEnemy.z) * forceHeavyAttack, ForceMode.Impulse);

            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 9)
        {
            attackSuceed = true;
            player.sound.LaunchHeavyAttack();
            Vector3 VecMoon = other.transform.position - posPlayer.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(VecMoon.x, 0, VecMoon.z) * forceHeavyAttackToMoon, ForceMode.Impulse);
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 13)
        {
            Vector3 VecBox = other.transform.position - posPlayer.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(VecBox.x, 0, VecBox.z) * forceHeavyAttack, ForceMode.Impulse);
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        if(!attackSuceed)
            player.sound.LaunchMissedAttack();
    }
}
