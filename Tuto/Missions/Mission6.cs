using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission6 : MonoBehaviour
{
    [SerializeField] private TutoManager tutoManager;
    [SerializeField] private MissionManager manager;
    [SerializeField] private GameObject cross;
    [SerializeField] private Text text;
    [SerializeField] private List<GameObject> breakables;

    // Start is called before the first frame update
    void Start()
    {
        tutoManager.ActivateMove();
        cross.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"{(30 - breakables.Count).ToString()}/30";

        if (breakables.Count <= 0)
        {
            cross.SetActive(true);
            manager.EndMission6();
        }
    }
}
