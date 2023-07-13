using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission7 : MonoBehaviour
{
    [SerializeField] private TutoManager tutoManager;
    [SerializeField] private TutoBuffer buffer;
    [SerializeField] private MissionManager manager;
    [SerializeField] private GameObject cross1;
    [SerializeField] private GameObject cross2;
    [SerializeField] private GameObject cross3;
    [SerializeField] private GameObject cross4;

    private bool goal1 = false;
    private bool goal2 = false;
    private bool goal3 = false;
    private bool goal4 = false;


    // Start is called before the first frame update
    void Start()
    {
        tutoManager.comboOn = true;
        cross1.SetActive(false);
        cross2.SetActive(false);
        cross3.SetActive(false);
        cross4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        tutoManager.comboOn = true;

        if (buffer.jabDone)
        {
            cross1.SetActive(true);
            goal1 = true;
        }

        if (buffer.spinDone)
        {
            cross2.SetActive(true);
            goal2 = true;
        }

        if (buffer.slamDone)
        {
            cross3.SetActive(true);
            goal3 = true;
        }

        if (buffer.uppercutDone)
        {
            cross4.SetActive(true);
            goal4 = true;
        }

        if (goal1 && goal2/* && goal3 */&& goal4)
        {
            manager.EndMission5();
        }
    }
}
