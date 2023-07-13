using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script used to store information among the scene
public class SceneData : MonoBehaviour
{
    public int nbEnemy;
    public bool inArena;
    public bool spawnAsStarted;

    private void Start()
    {
        inArena = false;
        nbEnemy = 0;
        spawnAsStarted = false;
    }
}
