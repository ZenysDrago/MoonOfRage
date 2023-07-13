using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;



public class Enemy : MonoBehaviour
{
    public float life;
    public int score;
    private new CameraScript camera;
    private SceneData sceneData;
    [SerializeField] private GameObject vfxDeadPrefab;
    [SerializeField] private VisualEffect vfxHit;
    [SerializeField] private VisualEffect vfxHitHard;
    [SerializeField] private int velocityNeed = 15;
    [SerializeField] private SoundEnemy sound;
    [SerializeField] private EnemyAnimator anim;

    [HideInInspector] public Rigidbody rbody;

    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        sceneData = FindObjectOfType<SceneData>();
        camera = FindObjectOfType<CameraScript>();
        sceneData.nbEnemy++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            MoonHit(collision.gameObject);
        }

    }


    private void LaunchParticleEffect(float damage)
    {
        if (damage < velocityNeed)
        {
            vfxHit.SendEvent("EnemyHitWeakVFX");
            sound.LaunchHit();
            anim.Hit();
        }
        else if (damage >= velocityNeed)
        {
            vfxHitHard.SendEvent("EnemyHitVFX");
            sound.LaunchBigHit();
            anim.BigHit();
        }

    }

    public void MoonHit(GameObject moon)
    {
        Rigidbody moonBody = moon.GetComponent<Rigidbody>();

        float vel = moonBody.velocity.magnitude;
        Vector3 dir = moonBody.transform.position - transform.position;
        rbody.AddForce(dir.normalized * vel, ForceMode.Impulse);

        GetHit(vel);
    }

    public void GetHit(float damage)
    {
        life -= damage;

        if (life <= 0)
        {
            GameObject objectVFX = Instantiate(vfxDeadPrefab, transform.position , transform.rotation);
            objectVFX.GetComponentInChildren<VisualEffect>().SendEvent("EnemyKillVFX");
            if(!camera.shakeCam)
            {
                camera.shakeCam = true;
                camera.shakeAmount = damage/100;
                camera.shakeDuration = 0.2f;
                camera.decreaseFactor = 1;
                StartCoroutine(camera.ShakeCamera());
            }
            Destroy(gameObject);
        }
        else
        {
            LaunchParticleEffect(damage);
            if (!camera.shakeCam)
            {
                camera.shakeCam = true;
                camera.shakeDuration = 0.5f;
                StartCoroutine(camera.ShakeCamera());
            }
        }
    }

    public void Death()
    {
        
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        Player player = FindObjectOfType<Player>();
        player.score += score * player.currentCombo;
        player.currentCombo++;
        player.comboTimer = 0;
        player.kills += 1;
        sceneData.nbEnemy--;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            anim.Grounded(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            anim.Grounded(false);
    }
}
