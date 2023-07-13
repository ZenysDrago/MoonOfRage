using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemies : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabEnemy;
    [Range(0, 100)]
    public int[] nbEnemy;
    [SerializeField] private float[] gapBetweenSpawn;
    private SceneData sceneData;

    public bool accelrationSpawn;
    [HideInInspector] public bool actioSpawner = false;
    private GameObject player;
    private Vector3 positionSpawner;
    public float timer;
    private int j = 0;
    private int i = 0;
    public float timeBetweenSpawn;

    private void Start()
    {
        //actioSpawner = false;
        accelrationSpawn = false;
        player = FindObjectOfType<Player>().gameObject;
        positionSpawner = transform.position;
        sceneData = FindObjectOfType<SceneData>();
    }

    private void Update()
    {
        
        if (actioSpawner)
        {

            if (accelrationSpawn)
                timeBetweenSpawn = gapBetweenSpawn[j] / 5;
            else
                timeBetweenSpawn = gapBetweenSpawn[j];


            timer += Time.deltaTime;
            if (timer > timeBetweenSpawn)
            {
                timer = 0;
                Instantiate(prefabEnemy[j], positionSpawner, Quaternion.identity);
                if (!sceneData.spawnAsStarted)
                    sceneData.spawnAsStarted = true;
                i++;
                if (i == nbEnemy[j])
                {
                    j++;
                    i = 0;  
                }
            }
            if (j == prefabEnemy.Length)
                actioSpawner = false;
        }
    }
}
