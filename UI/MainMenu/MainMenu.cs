using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MenuManager manager;

    public void Continue()
    {
        //Choose between the three saves and launch the level saved
        manager.state = MenuState.SaveMenu;
        manager.fromContinue = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(manager.firstButtonSave);
    }

    public void NewGame()
    {
        //Launch level 1 er tutorial scene
        manager.state = MenuState.SaveMenu;
        manager.fromContinue = false;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(manager.firstButtonSave);
    }

    public void LevelSelection()
    {
        manager.state = MenuState.LevelSelection;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(manager.firstButtonLevelSel);
    }

    public void Option()
    {
        manager.state = MenuState.Option;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(manager.firstButtonOption);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
