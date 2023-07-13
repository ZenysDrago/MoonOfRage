using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission5 : MonoBehaviour
{
    [SerializeField] private TutoManager tutoManager;
    [SerializeField] private MissionManager manager;
    [SerializeField] private TutoChainControl chainControl;
    [SerializeField] private GameObject cross;

    // Start is called before the first frame update
    void Start()
    {
        tutoManager.ActivateMoon();
        tutoManager.ActivateRecall();
        cross.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (chainControl.hasRecalled)
        {
            cross.SetActive(true);
            manager.EndMission4();
        }
    }
}
