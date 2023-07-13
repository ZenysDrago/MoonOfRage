using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Slider lifeBar;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI comboMax;
    [SerializeField] private TextMeshProUGUI combo;
    [SerializeField] private TextMeshProUGUI timer;
    private int minutes = 0;
    private int secondes = 0;
    private int time = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time = (int)player.levelTimer;
        minutes = time / 60;
        secondes = time - minutes * 60;
        lifeBar.value = player.life * 100 / player.maxLife;
        score.text = $"{player.score}";
        comboMax.text = $"{player.maxCombo}";
        combo.text = $"x{player.currentCombo}";

        if (secondes < 10)
            timer.text = $"{minutes}:0{secondes}";
        else
            timer.text = $"{minutes}:{secondes}";
    }
}
