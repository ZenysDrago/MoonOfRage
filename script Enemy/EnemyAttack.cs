using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float forceAttack;
    public int damage;
    Vector3 vecEnemy = new Vector3();
    public Vector3 enemyPos;
    //private PlayerController player;

    //private Transform posEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            vecEnemy = other.transform.position - enemyPos;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(vecEnemy.x, 0, vecEnemy.z) * forceAttack, ForceMode.Impulse);
            other.gameObject.GetComponent<Player>().life -= damage;
        }
        Destroy(gameObject);
        
    }
}
