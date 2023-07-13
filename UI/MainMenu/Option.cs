using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class Option : MonoBehaviour
{
    [SerializeField] private MenuManager manager;
    [SerializeField] private Toggle windowedToggle;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterVolSlider;

    // Start is called before the first frame update
    void Start()
    {
        windowedToggle.isOn = !Screen.fullScreen;

        if (PlayerPrefs.HasKey("MasterVol"))
            masterVolSlider.value = PlayerPrefs.GetFloat("MasterVol");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMasterVolume()
    {
        //slyder
        mixer.SetFloat("MasterVol", masterVolSlider.value);
        PlayerPrefs.SetFloat("MasterVol", masterVolSlider.value);
    }

    public void SetWindowed()
    {
        //choose between severals resolutions
        Screen.fullScreen = !windowedToggle.isOn;
    }

    public void ToControlsMenu()
    {
        manager.state = MenuState.ControlsMenu;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(manager.firstButtonControls);
    }

    public void Back()
    {
        manager.state = MenuState.MainMenu;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(manager.firstButtonMain);
    }
}
