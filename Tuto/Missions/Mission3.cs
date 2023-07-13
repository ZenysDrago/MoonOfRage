using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission3 : MonoBehaviour
{
    [SerializeField] private GameObject cross1;
    [SerializeField] private GameObject cross2;
    [SerializeField] private MissionManager manager;
    [SerializeField] private Player player;
    [SerializeField] private TutoControls controls;
    [SerializeField] private bool goal1 = false;
    [SerializeField] private bool goal2 = false;
    // Start is called before the first frame update
    void Start()
    {
        cross1.SetActive(false);
        cross2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.isGrounded)
        {
            player.state = PlayerState.STUNNED;
        }

        if (controls.hasGetUp)
        {
            cross1.SetActive(true);
            goal1 = true;
        }

        if (controls.hasRolled)
        {
            cross2.SetActive(true);
            goal2 = true;
        }

        if (goal1 && goal2)
            manager.EndMission3();
    }
}
