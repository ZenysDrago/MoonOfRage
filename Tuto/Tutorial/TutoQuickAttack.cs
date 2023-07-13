using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoQuickAttack : MonoBehaviour
{
    public float damage = 5f;
    public float forceQuickAttack = 5f;
    public float forceQuickAttackToMoon = 15f;

    private bool attackSuceed = false;
    private Transform posPlayer;
    private TutoControls player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<TutoControls>();
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
            player.sound.LaunchAttack();
            other.gameObject.GetComponent<Enemy>().GetHit(damage);

            Vector3 VecEnemy = other.transform.position - posPlayer.position;
            other.gameObject.GetComponent<Enemy>().rbody.AddForce(VecEnemy * forceQuickAttack, ForceMode.Impulse);

            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 9)
        {
            attackSuceed = true;
            player.sound.LaunchAttack();
            Vector3 VecMoon = other.transform.position - posPlayer.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(VecMoon * forceQuickAttackToMoon, ForceMode.Impulse);
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 13)
        {
            Vector3 VecBox = other.transform.position - posPlayer.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(VecBox.x, 0, VecBox.z) * forceQuickAttack, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        /*
        if (!attackSuceed)
            player.sound.LaunchMissedAttack();
        */
    }
}
