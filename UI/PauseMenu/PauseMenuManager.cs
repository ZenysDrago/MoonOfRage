using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{

    public MenuState pauseState;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject controlOption;

    public GameObject firstButtonMain;
    public GameObject firstButtonOption;
    public GameObject firstButtonControl;

    // Start is called before the first frame update
    void Start()
    {
        pauseState = MenuState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        switch (pauseState)
        {
            case MenuState.MainMenu:
                pauseMenu.SetActive(true);
                option.SetActive(false);
                controlOption.SetActive(false);
                break;
            case MenuState.Option:
                pauseMenu.SetActive(false);
                option.SetActive(true);
                controlOption.SetActive(false);
                break;
            case MenuState.ControlsMenu:
                pauseMenu.SetActive(false);
                option.SetActive(false);
                controlOption.SetActive(true);
                break;
            default:
                return;
        }
    }
}
