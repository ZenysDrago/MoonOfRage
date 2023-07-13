using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uppercutAttack : MonoBehaviour
{
    public float forceUppercutAttack = 20f;
    public float forceUppercutAttackToMoon = 100f;
    public float damage = 5f;

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
            player.sound.LaunchUppercut();
            other.gameObject.GetComponent<Enemy>().GetHit(damage);
            //other.gameObject.GetComponent<Enemy>().rbody.AddForce(Vector3.up * forceUppercutAttack, ForceMode.Impulse);

            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 9)
        {
            attackSuceed = true;
            player.sound.LaunchUppercut();
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * forceUppercutAttackToMoon, ForceMode.Impulse);
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 13)
        {
            Vector3 VecBox = other.transform.position - posPlayer.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(VecBox.x, 0, VecBox.z) * forceUppercutAttack, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(!attackSuceed)
            player.sound.LaunchMissedAttack();
    }
}
