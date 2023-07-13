using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformToArene : MonoBehaviour
{
    new CameraScript camera;
    [SerializeField] private SpawnerEnemies[] spawner;
    [SerializeField] private int minNbEnemy;
    [SerializeField] private InitSound init;
    [SerializeField] private GameObject classicUI;
    [SerializeField] private GameObject bloodMoonUI;
    [SerializeField] private SceneData sceneData;
    [SerializeField] private float posACamX, posACamY, posACamZ;

    [SerializeField] private  GameObject[] Wall;
    private bool asPassed = false;

    //[SerializeField] public nbEnemies nbEnemies;
    [HideInInspector] public int compteurAllEnemy;
    public float coolDownArene;
    public bool bloodMoon = false;
    public int nbSpawnerStop;

    // Start is called before the first frame update
    void Start()
    {
        nbSpawnerStop = 0;
        camera = FindObjectOfType<CameraScript>();
        init = FindObjectOfType<InitSound>();
        sceneData = FindObjectOfType<SceneData>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNbEnemy();

        UpdateCamera();

        CoolDownArene();
    }


    private void UpdateNbEnemy()
    {
        for (int i = 0; i < spawner.Length; i++)
        {
            spawner[i].accelrationSpawn = (sceneData.nbEnemy <= minNbEnemy);
        }
    }

    private void CoolDownArene()
    {
        if (sceneData.inArena == true)
        {
            if (coolDownArene > 0)
                coolDownArene -= Time.deltaTime;
            else
                sceneData.inArena = false;
        }
        else
        {
            for (int i = 0; i < Wall.Length; i++)
            {
                Wall[i].SetActive(false);
            }
        }

    }
    private void UpdateCamera()
    {
        if (!sceneData.spawnAsStarted)
            return;

        sceneData.inArena = sceneData.nbEnemy != 0;

        if (!sceneData.inArena && asPassed)
        {
            Debug.Log("End arena");
            init.LaunchTransition();
            nbSpawnerStop = 0;
            for (int i = 0; i < spawner.Length; i++)
            {
                
                if (spawner[i].actioSpawner == false)
                {
                    nbSpawnerStop++;
                }
            }
            if (nbSpawnerStop >= spawner.Length)
            {
                classicUI.SetActive(true);
                bloodMoonUI.SetActive(false);

                foreach (GameObject wall in Wall)
                {
                    Debug.Log("Wall cleared");
                    wall.SetActive(false);
                }
            }
            sceneData.spawnAsStarted = false;
        }
    }

    private void ActiveArene()
    {
        Debug.Log("Active Arena");
        sceneData.inArena = true;
        camera.posACamX = posACamX;
        camera.posACamY = posACamY;
        camera.posACamZ = posACamZ;

        init.LaunchTransition();
        if (bloodMoon)
        {
            classicUI.SetActive(false);
            bloodMoonUI.SetActive(true);
        }
        else
        {
            classicUI.SetActive(true);
            bloodMoonUI.SetActive(false);
        }


        for (int i = 0; i < Wall.Length; i++)
        {
            Wall[i].SetActive(true);
        }
        //camera.posACamY += (int)transform.position.y;
        camera.CamHeight = camera.player.transform.position.y;
        for (int i = 0; i < spawner.Length; i++)
        {
            spawner[i].actioSpawner = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (!asPassed)
            {
                ActiveArene();
                camera.posCollider = transform.position;
                asPassed = true;
            }
        }

    }
}
