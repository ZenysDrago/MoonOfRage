using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission9 : MonoBehaviour
{
    [SerializeField] private TutoManager tutoManager;
    [SerializeField] private TutoBuffer buffer;
    [SerializeField] private MissionManager manager;
    [SerializeField] private GameObject cross1;
    [SerializeField] private GameObject cross2;
    [SerializeField] private GameObject cross3;

    private bool goal1 = false;
    private bool goal2 = false;
    private bool goal3 = false;


    // Start is called before the first frame update
    void Start()
    {
        tutoManager.ActivateFinisher();
        cross1.SetActive(false);
        cross2.SetActive(false);
        cross3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (buffer.yoyoDone)
        {
            goal1 = true;
            cross1.SetActive(true);
        }

        if (buffer.superSpinDone)
        {
            goal2 = true;
            cross2.SetActive(true);
        }

        if (buffer.suppercutDone)
        {
            goal3 = true;
            cross3.SetActive(true);
        }

        if (goal1 && goal2 && goal3)
        {
            manager.EndMission7();
        }
    }
}
