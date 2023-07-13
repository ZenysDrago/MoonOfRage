using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BoomEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeAtt;
    [SerializeField] private float timeStun;
    [SerializeField] private float forceAttack;
    [SerializeField] private int damage;
    [SerializeField] private SoundBomb sound;
    [SerializeField] GameObject enemyBoomHitbox;
    private float timerAttack;
    private float timerStun;
    private bool enemyCanAttack;
    private bool isStun;
    private GameObject enemyBoomAttack;

    

    [HideInInspector] public Rigidbody rbody;
    private Player player;

    [SerializeField] Material attMaterial;
    [SerializeField] Material basicMaterial;
    private MeshRenderer mesh;

    private Vector3 distPlayer;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        enemyCanAttack = true;
        isStun = false;
    }

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = rbody.velocity;
        FollowPlayer();
        player = FindObjectOfType<Player>();
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0);
        AttackUpdate();
        //enemyBoomAttack = GetComponent<EnemyBoomHitbox>();
    }

    void AttackUpdate()
    {
        if (timerAttack > 0)
        {
            timerAttack -= Time.deltaTime;
            if (timerAttack < 0)
            {
                EnemyAttack();
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //.Log("ddfghdfgh");
            if(enemyCanAttack)
            {
                sound.LaunchFireUp();
                timerAttack = timeAtt;
                enemyCanAttack = false;
            }
            //mesh.material = attMaterial;
        }

    }

    private void EnemyAttack()
    {
        
        enemyBoomAttack = Instantiate(enemyBoomHitbox, transform.position, Quaternion.identity);
        enemyBoomAttack.GetComponent<EnemyBoomAttack>().enemyPos = transform.position;
        enemyBoomAttack.GetComponent<EnemyBoomAttack>().damage = damage;
        enemyBoomAttack.GetComponent<EnemyBoomAttack>().forceAttack = forceAttack;
        timerAttack = 0;
        //mesh.material = basicMaterial;
        enemyCanAttack = true;
        Destroy(gameObject);
        
    }

    private void FollowPlayer()
    {
        distPlayer = new Vector3(player.transform.position.x - transform.position.x, rbody.velocity.y, player.transform.position.z - transform.position.z);
        if(player.state != PlayerState.STUNNED)
            timerStun = 0;

        if (player.state == PlayerState.STUNNED && timerStun == 0)
            timerStun = timeStun;


        if (timerStun > 0)
        {
            timerStun -= Time.deltaTime;
            rbody.velocity = -distPlayer.normalized * speed / 1.5f * Time.deltaTime;
        }
        else
        {
            rbody.velocity = distPlayer.normalized * speed * Time.deltaTime;
        }
        rbody.velocity = new Vector3(rbody.velocity.x, -10, rbody.velocity.z);
    }

    public void DeathBoomEnemy()
    {
        EnemyAttack();
    }
}

