using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PauseMenuManager manager;
    [SerializeField] private GameObject menu;
    [SerializeField] private Player player;

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Option()
    {
        manager.pauseState = MenuState.Option;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(manager.firstButtonOption);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    public void QuitToMenu()
    {
        player.timePause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
