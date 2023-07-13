using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Victory : MonoBehaviour
{
    [SerializeField] private LevelEnd end;
    [SerializeField] private TextMeshProUGUI textRank;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textHighCombo;
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private TextMeshProUGUI textKills;

    private Player player;
    private int minutes = 0;
    private int secondes = 0;
    private int time = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        player.timePause = true;
        player.GetComponent<PlayerInput>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time = (int)player.levelTimer;
        minutes = time / 60;
        secondes = time - minutes * 60;

        if (player.score >= end.scoreForS)
            textRank.text = $"s";
        if (player.score >= end.scoreForA)
            textRank.text = $"a";
        if (player.score >= end.scoreForB)
            textRank.text = $"b";
        if (player.score >= end.scoreForC)
            textRank.text = $"c";
        if (player.score >= end.scoreForD)
            textRank.text = $"d";

        textScore.text = $"final score : {player.score}";
        textHighCombo.text = $"max combo : {player.maxCombo}";
        textKills.text = $"Foes killed : {player.kills}";

        if (secondes < 10)
            textTimer.text = $"time taken : {minutes}:0{secondes}";
        else
            textTimer.text = $"time taken : {minutes}:{secondes}";
    }

    public void Next()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Checkpoint", 0);
        SceneManager.LoadScene(end.nextLevel);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
