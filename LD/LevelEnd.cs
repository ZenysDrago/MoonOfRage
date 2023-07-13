using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject firstButtonVictory;
    public int scoreForD;
    public int scoreForC;
    public int scoreForB;
    public int scoreForA;
    public int scoreForS;
    private int currentSave;
    public int nextLevel;

    private void Start()
    {
        victory.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (PlayerPrefs.HasKey("CurrentSave"))
                currentSave = PlayerPrefs.GetInt("CurrentSave");
            else
                currentSave = 0;

            nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextLevel > 6)
                nextLevel = 6;

            switch (currentSave)
            {
                case 1:
                    PlayerPrefs.SetInt("Save1Level", nextLevel);
                    break;
                case 2:
                    PlayerPrefs.SetInt("Save1Leve2", nextLevel);
                    break;
                case 3:
                    PlayerPrefs.SetInt("Save1Leve3", nextLevel);
                    break;
                default:
                    break;
            }

            victory.SetActive(true);
            Time.timeScale = 0.5f;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonVictory);
        }
    }
}
