using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveMenu : MonoBehaviour
{
    [SerializeField] private MenuManager manager;
    [SerializeField] private GameObject verificationMenu;

    private int saveNumb = 1;
    // Start is called before the first frame update
    void Start()
    {
        verificationMenu.SetActive(false);
    }

    public void Save1()
    {
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.SetInt("CurrentSave", 1);

        if (manager.fromContinue)
        {
            // read save1 file and launch the level scene

            if (PlayerPrefs.HasKey("Save1Level") && PlayerPrefs.GetInt("Save1Level") >= 1)
            {
                manager.levelLaunched = PlayerPrefs.GetInt("Save1Level");
            }
            else
            {
                PlayerPrefs.SetInt("Save1Level", 1);
                manager.levelLaunched = PlayerPrefs.GetInt("Save1Level");
            }

            manager.state = MenuState.LoadingScreen;
        }
        else
        {
            // set active are you sure panel 
            // if yes, reset save1
            verificationMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(manager.firstButtonVerification);
        }
    }

    public void Save2()
    {
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.SetInt("CurrentSave", 2);

        if (manager.fromContinue)
        {


            if (PlayerPrefs.HasKey("Save2Level") && PlayerPrefs.GetInt("Save2Level") >= 1)
            {
                manager.levelLaunched = PlayerPrefs.GetInt("Save2Level");
            }
            else
            {
                PlayerPrefs.SetInt("Save2Level", 1);
                manager.levelLaunched = PlayerPrefs.GetInt("Save2Level");
            }
            // read save2 file and launch the level scene
            manager.state = MenuState.LoadingScreen;
        }
        else
        {
            // set active are you sure panel 
            // if yes, reset save1
            verificationMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(manager.firstButtonVerification);
        }
    }

    public void Save3()
    {
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.SetInt("CurrentSave", 3);

        if (manager.fromContinue)
        {

            if (PlayerPrefs.HasKey("Save3Level") && PlayerPrefs.GetInt("Save3Level") >= 1)
            {
                manager.levelLaunched = PlayerPrefs.GetInt("Save3Level");
            }
            else
            {
                PlayerPrefs.SetInt("Save3Level", 1);
                manager.levelLaunched = PlayerPrefs.GetInt("Save3Level");
            }
            // read save3 file and launch the level scene
            manager.state = MenuState.LoadingScreen;
        }
        else
        {
            // set active are you sure panel 
            // if yes, reset save1
            verificationMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(manager.firstButtonVerification);
        }
    }

    public void Back()
    {
        manager.state = MenuState.MainMenu;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(manager.firstButtonMain);
    }

    public void Verification()
    {
        if (saveNumb == 1)
        {
            PlayerPrefs.SetInt("Save1Level", 1);
            manager.levelLaunched = PlayerPrefs.GetInt("Save1Level");
            manager.state = MenuState.LoadingScreen;
            verificationMenu.SetActive(false);
        }
        else if (saveNumb == 2)
        {
            PlayerPrefs.SetInt("Save2Level", 1);
            manager.levelLaunched = PlayerPrefs.GetInt("Save2Level");
            manager.state = MenuState.LoadingScreen;
            verificationMenu.SetActive(false);
        }
        else if (saveNumb == 3)
        {
            PlayerPrefs.SetInt("Save3Level", 1);
            manager.levelLaunched = PlayerPrefs.GetInt("Save3Level");
            manager.state = MenuState.LoadingScreen;
            verificationMenu.SetActive(false);
        }
    }

    public void Cancel()
    {
        verificationMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(manager.firstButtonSave);
    }
}
