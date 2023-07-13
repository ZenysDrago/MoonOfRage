using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 : MonoBehaviour
{
    [SerializeField] private GameObject cross;
    [SerializeField] private TutoControls control;
    [SerializeField] private MissionManager manager;
    [SerializeField] private int runTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        cross.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (control.movingTime >= runTime)
        {
            cross.SetActive(true);
            //Wait 0.5 sec
            manager.EndMission1();
        }
    }
}
