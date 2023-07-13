using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission4 : MonoBehaviour
{
    [SerializeField] private List<GameObject> breakables;
    [SerializeField] private MissionManager manager;
    [SerializeField] private TutoManager tutoManager;
    [SerializeField] private Player player;
    [SerializeField] private GameObject cross;
    [SerializeField] private Text text;
    // Start is called before the first frame update
    void Start()
    {
        player.state = PlayerState.CLASSIC;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"{(30 - breakables.Count).ToString()}/30";
        if (breakables.Count == 0)
        {
            cross.SetActive(true);
            manager.EndMission5();
        }
    }
}
