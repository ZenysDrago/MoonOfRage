using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission8 : MonoBehaviour
{
    [SerializeField] private TutoManager tutoManager;
    [SerializeField] private TutoFight fight;
    [SerializeField] private MissionManager manager;
    [SerializeField] private GameObject cross1;
    [SerializeField] private GameObject cross2;
    [SerializeField] private Text text1;
    [SerializeField] private Text text2;

    private bool goal1 = false;
    private bool goal2 = false;


    // Start is called before the first frame update
    void Start()
    {
        cross1.SetActive(false);
        cross2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        text1.text = $"{fight.uppercutCount.ToString()}/3";
        text2.text = $"{fight.stompCount.ToString()}/3";

        if (fight.uppercutCount >= 3)
        {
            goal1 = true;
            cross1.SetActive(true);
        }

        if (fight.stompCount >= 3)
        {
            goal2 = true;
            cross2.SetActive(true);
        }

        if (goal1 && goal2)
        {
            manager.EndMission6();
        }
    }
}
