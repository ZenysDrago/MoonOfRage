using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission10 : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
