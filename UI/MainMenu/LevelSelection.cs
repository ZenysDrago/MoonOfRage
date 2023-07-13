using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private MenuManager manager;

    public void Level1()
    {
        //Launch level1 scene
        PlayerPrefs.SetInt("CurrentSave", 0);
        PlayerPrefs.SetInt("Checkpoint", 0);
        manager.levelLaunched = 2;
        manager.state = MenuState.LoadingScreen;
    }

    public void Level2()
    {
        //lanch level2 scene
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.SetInt("CurrentSave", 0);
        manager.levelLaunched = 3;
        manager.state = MenuState.LoadingScreen;
    }

    public void Level3()
    {
        //lanch level3 scene
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.SetInt("CurrentSave", 0);
        manager.levelLaunched = 4;
        manager.state = MenuState.LoadingScreen;
    }

    public void Level4()
    {
        //launch level4 scene
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.SetInt("CurrentSave", 0);
        manager.levelLaunched = 5;
        manager.state = MenuState.LoadingScreen;
    }

    public void Level5()
    {
        //launch level5 scene
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.SetInt("CurrentSave", 0);
        manager.levelLaunched = 6;
        manager.state = MenuState.LoadingScreen;
    }

    public void Back()
    {
        manager.state = MenuState.MainMenu;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(manager.firstButtonMain);
    }
}
