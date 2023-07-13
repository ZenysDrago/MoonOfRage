using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> missions;
    [SerializeField] private GameObject End;

    // Start is called before the first frame update
    void Start()
    {
        missions[0].SetActive(true);
        End.SetActive(false);

        for (int i = 1; i < missions.Count; i++)
        {
            missions[i].SetActive(false);
        }
    }

    public void EndMission1()
    {
        missions[0].SetActive(false);
        missions[1].SetActive(true);
    }

    public void EndMission2()
    {
        missions[1].SetActive(false);
        missions[2].SetActive(true);
    }

    public void EndMission3()
    {
        missions[2].SetActive(false);
        missions[3].SetActive(true);
    }

    public void EndMission4()
    {
        missions[3].SetActive(false);
        missions[4].SetActive(true);
    }

    public void EndMission5()
    {
        missions[4].SetActive(false);
        missions[5].SetActive(true);
    }

    public void EndMission6()
    {
        missions[5].SetActive(false);
        missions[6].SetActive(true);
    }

    public void EndMission7()
    {
        missions[6].SetActive(false);
        End.SetActive(true);
    }
}
