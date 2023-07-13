using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeAtt;
    [SerializeField] private float timeBeforeAtt;
    [SerializeField] private int damage;


    [SerializeField] Material attMaterial;
    [SerializeField] Material basicMaterial;
    private MeshRenderer mesh;

    [HideInInspector] public Rigidbody rbody;
    private Player player;

    private Vector3 distPlayer;
    private Vector3 direction;
    private float Y = 0;
    private float timerBeforeAtt;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
        timerBeforeAtt = timeBeforeAtt;
    }

    // Update is called once per frame
    void Update()
    {
        direction = rbody.velocity;
        player = FindObjectOfType<Player>();
        distPlayer = new Vector3(player.transform.position.x - transform.position.x, Y, player.transform.position.z - transform.position.z);
        
        

        if (timerBeforeAtt > 0)
        {
            timerBeforeAtt -= Time.deltaTime;
            FlyAroundPlayer();
        }
        else
        {
            EnemyAttack();
        }
    }

    private void FollowPlayer()
    {
        if (player.state == PlayerState.STUNNED)
        {
            rbody.velocity = -distPlayer.normalized * speed / 1.5f * Time.deltaTime;
        }
        else
        {
            rbody.velocity = distPlayer.normalized * speed * Time.deltaTime;
        }
    }

    private void FlyAroundPlayer()
    {
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 90);
        if (distPlayer.x < 5 && distPlayer.z < 5)
            rbody.velocity = -distPlayer.normalized * speed / 1.5f * Time.deltaTime;
        else if(distPlayer.x > 6 && distPlayer.z > 6)
            rbody.velocity = distPlayer.normalized * speed * Time.deltaTime;
        transform.RotateAround(player.transform.position, Vector3.up, 90 * Time.deltaTime);
    }

    private void EnemyAttack()
    {
        FollowPlayer();
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 90, 180*(-Mathf.Abs(distPlayer.x) - Mathf.Abs(distPlayer.y) - Mathf.Abs(distPlayer.z)) /60);
        Y = player.transform.position.y - transform.position.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            player.life -= damage;
            timerBeforeAtt = timeBeforeAtt;
        }

    }
}
