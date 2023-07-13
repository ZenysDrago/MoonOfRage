using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum PlayerState
{
    CLASSIC,
    STUNNED,
    CONTROLLING_MOON,
    COMBO_MOON,
    PUSHING_MOON,
}

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject firstPauseButton;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject sphere;
    [SerializeField] private AudioSource audioDeath;

    public List<RespawnPoint> checkpoints;
 
    [Tooltip("hello"), Range(0, 1000)]
    public int life = 10;
    public int maxLife;
    public int score = 0;
    public int kills = 0;
    public int currentCombo = 0;
    public int maxCombo = 0;
    public float comboTimer = 0;
    public float levelTimer = 0;
    public bool timePause = false;
    [SerializeField] private float resetTime = 10;

    public float stunTimer;
    public PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        //life = 10;
        maxLife = life;
        stunTimer = 0;
        state = PlayerState.CLASSIC;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
            Death();

        if (currentCombo > maxCombo)
            maxCombo = currentCombo;

        if (stunTimer > 0)
        {
            state = PlayerState.STUNNED;
            stunTimer -= Time.deltaTime;
        }

        comboTimer += Time.deltaTime;
        if (comboTimer > resetTime)
            currentCombo = 0;

        if (!timePause)
            levelTimer += Time.deltaTime;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        timePause = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstPauseButton);
    }

    public void Death()
    {
        GetComponent<PlayerInput>().enabled = false;
        Time.timeScale = 0.5f;
        audioDeath.Play();
        gameOver.SetActive(true);
    }
}
