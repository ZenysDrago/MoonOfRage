using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission2 : MonoBehaviour
{
    [SerializeField] private GameObject cross1;
    [SerializeField] private Text text1;
    [SerializeField] private GameObject cross2;
    [SerializeField] private Text text2;
    [SerializeField] private TutoControls control;
    [SerializeField] private TutoManager tutoManager;
    [SerializeField] private MissionManager manager;
    [SerializeField] private bool goal1 = false;
    [SerializeField] private bool goal2 = false;

    // Start is called before the first frame update
    void Start()
    {
        tutoManager.ActivateJump();
        cross1.SetActive(false);
        cross2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        text1.text = $"{control.jumpCount.ToString()}/3";
        text2.text = $"{control.doubleJumpCount.ToString()}/3";

        if (control.jumpCount >= 3)
        {
            cross1.SetActive(true);
            goal1 = true;
        }

        if (control.doubleJumpCount >= 3)
        {
            cross2.SetActive(true);
            goal2 = true;
        }

        if (goal1 && goal2)
            manager.EndMission2();
    }
}
