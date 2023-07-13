using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public enum MenuState
{
    TitleScreen,
    MainMenu,
    LevelSelection,
    Option,
    SaveMenu,
    ControlsMenu,
    LoadingScreen,
}

public class MenuManager : MonoBehaviour
{
    public MenuState state;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelection;
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject saveMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject loadingScreen;

    public GameObject firstButtonMain;
    public GameObject firstButtonLevelSel;
    public GameObject firstButtonOption;
    public GameObject firstButtonSave;
    public GameObject firstButtonVerification;
    public GameObject firstButtonControls;

    [SerializeField] private Camera cam;
    [SerializeField] private Transform titleCam;
    [SerializeField] private Transform mainCam;
    [SerializeField] private Transform optionCam;


    public bool fromContinue;
    public int levelLaunched;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = MenuState.TitleScreen; 
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case MenuState.TitleScreen:
                SetTitleScreen();
                break;

            case MenuState.MainMenu:
                SetMainMenu();
                break;

            case MenuState.Option:
                SetOption();
                break;

            case MenuState.LevelSelection:
                SetLevelSelection();
                break;

            case MenuState.SaveMenu:
                SetSaveMenu();
                break;

            case MenuState.ControlsMenu:
                SetControls();
                break;

            case MenuState.LoadingScreen:
                SetLoadingScreen();
                break;
            default:
                return;
        }
    }

    private void SetTitleScreen()
    {
        cam.transform.position = titleCam.position;
        cam.transform.rotation = titleCam.rotation;

        titleScreen.SetActive(true);
        mainMenu.SetActive(false);
        levelSelection.SetActive(false);
        option.SetActive(false);
        saveMenu.SetActive(false);
        controlsMenu.SetActive(false);
        loadingScreen.SetActive(false);
    }

    private void SetMainMenu()
    {
        cam.transform.position = mainCam.position;
        cam.transform.rotation = mainCam.rotation;

        titleScreen.SetActive(false);
        mainMenu.SetActive(true);
        levelSelection.SetActive(false);
        option.SetActive(false);
        controlsMenu.SetActive(false);
        loadingScreen.SetActive(false);
        saveMenu.SetActive(false);
    }

    private void SetOption()
    {
        cam.transform.position = optionCam.position;
        cam.transform.rotation = optionCam.rotation;

        titleScreen.SetActive(false);
        mainMenu.SetActive(false);
        levelSelection.SetActive(false);
        saveMenu.SetActive(false);
        controlsMenu.SetActive(false);
        loadingScreen.SetActive(false);
        option.SetActive(true);
    }

    private void SetLevelSelection()
    {
        cam.transform.position = optionCam.position;
        cam.transform.rotation = optionCam.rotation;

        titleScreen.SetActive(false);
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
        saveMenu.SetActive(false);
        controlsMenu.SetActive(false);
        loadingScreen.SetActive(false);
        option.SetActive(false);
    }

    private void SetSaveMenu()
    {
        titleScreen.SetActive(false);
        mainMenu.SetActive(false);
        levelSelection.SetActive(false);
        saveMenu.SetActive(true);
        controlsMenu.SetActive(false);
        loadingScreen.SetActive(false);
        option.SetActive(false);
    }

    private void SetControls()
    {
        titleScreen.SetActive(false);
        mainMenu.SetActive(false);
        levelSelection.SetActive(false);
        saveMenu.SetActive(false);
        controlsMenu.SetActive(true);
        loadingScreen.SetActive(false);
        option.SetActive(false);
    }

    private void SetLoadingScreen()
    {
        titleScreen.SetActive(false);
        mainMenu.SetActive(false);
        levelSelection.SetActive(false);
        saveMenu.SetActive(false);
        controlsMenu.SetActive(false);
        loadingScreen.SetActive(true);
        option.SetActive(false);
    }



    public void TitleToMainMenu(InputAction.CallbackContext context)
    {
        if (context.performed && state == MenuState.TitleScreen)
        {
            state = MenuState.MainMenu;
        }
    }

    public void LoadingToGame(InputAction.CallbackContext context)
    {
        if (context.performed && state == MenuState.LoadingScreen)
        {
            SceneManager.LoadScene(levelLaunched);
        }
    }
}
