using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoomAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public SoundBomb sound;
    public float forceAttack;
    public int damage;
    Vector3 vecEnemy = new Vector3();
    public Vector3 enemyPos;


    private void OnTriggerEnter(Collider other)
    {
        CollisionEnemyBoom(other);
        CollisionPlayer(other);
        CollisionEnemy(other);

    }

    private void CollisionPlayer(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            sound.LaunchExplose();
            vecEnemy = other.transform.position;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(vecEnemy.x, 0, vecEnemy.z) * forceAttack, ForceMode.Impulse);
            other.gameObject.GetComponent<Player>().life -= damage;
            GetComponent<Enemy>().GetHit(100);
        }
    }

    private void CollisionEnemy(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            vecEnemy = other.transform.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(vecEnemy.x, 0, vecEnemy.z) * forceAttack, ForceMode.Impulse);
            other.gameObject.GetComponent<Enemy>().life -= damage;
            GetComponent<Enemy>().GetHit(100);
        }
    }

    private void CollisionEnemyBoom(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            vecEnemy = other.transform.position;
            other.gameObject.GetComponent<BoomEnemy>().DeathBoomEnemy();
        }
    }
}
