using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemySize
{
    Small,
    Medium,
    Tall,
}


public class GroundEnemy : MonoBehaviour
{
    [SerializeField] private EnemySize size;
    [SerializeField] private float speed;
    [SerializeField] private float timeAtt;
    [SerializeField] private float timeStun;
    [SerializeField] private float forceAttack;
    [SerializeField] private int damage;
    [SerializeField] private EnemyAnimator anim;
    [SerializeField] GameObject enemyAttackHitbox;
    [SerializeField] GameObject enemyBigAttackHitbox;
    private float timerAttack;
    private float timerStun;
    private bool enemyCanAttack;
    private bool isStun;
    private GameObject enemyBigAttack;
    private GameObject enemyAttack;
    private SoundEnemy sound;


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
        player = FindObjectOfType<Player>();
    }

    void Awake()
    {
        sound = GetComponent<SoundEnemy>();
        rbody = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = rbody.velocity;
        FollowPlayer();
        player = FindObjectOfType<Player>();
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0);
        AttackUpdate();
        //enemyBigAttack = GetComponent<EnemyBigAttack>();
    }

    void AttackUpdate()
    {
        if (timerAttack > 0)
        {
            LaunchAnim();
            timerAttack -= Time.deltaTime;
            if (timerAttack < 0)
            {
                sound.LaunchAttack();
                EnemyAttack();
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //.Log("ddfghdfgh");
            if (enemyCanAttack)
            {
                timerAttack = timeAtt;
                enemyCanAttack = false;
            }
            //mesh.material = attMaterial;
        }

    }

    private void EnemyAttack()
    {
        if (size == EnemySize.Tall)
        {
            enemyBigAttack = Instantiate(enemyBigAttackHitbox, transform.position + transform.forward * 4f, Quaternion.identity);
            enemyBigAttack.GetComponent<EnemyBigAttack>().enemyPos = transform.position;
            enemyBigAttack.GetComponent<EnemyBigAttack>().damage = damage;
            enemyBigAttack.GetComponent<EnemyBigAttack>().forceAttack = forceAttack;
        }
        else
        {
            enemyAttack = Instantiate(enemyAttackHitbox, transform.position + transform.forward * 1.5f, Quaternion.identity);
            enemyAttack.GetComponent<EnemyAttack>().enemyPos = transform.position;
            enemyAttack.GetComponent<EnemyAttack>().damage = damage;
            enemyAttack.GetComponent<EnemyAttack>().forceAttack = forceAttack;
        }
        timerAttack = 0;
        //mesh.material = basicMaterial;
        enemyCanAttack = true;

    }

    private void LaunchAnim()
    {
        if(size == EnemySize.Small)
        {
            int r = Random.Range(0, 1);
            anim.AttMinor(r == 0);
        }
        else
        {
            anim.Att1();
        }
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

        if (rbody.velocity != Vector3.zero)
        {
            sound.LaunchWalk();
            anim.Run(true);
        }
        else
        {
            anim.Run(false);
            sound.StopWalk();
        }
    }
}
