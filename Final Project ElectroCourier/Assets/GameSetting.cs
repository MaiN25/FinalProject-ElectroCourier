using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSetting : MonoBehaviour
{
    public static GameSetting instance = null;
    public GameObject settingPanel;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private TMP_Text volumeTextUI = null;
    [SerializeField] public GameObject mute;
    [SerializeField] public GameObject audioOn;
    AudioSource audioSource;
    //find the UI dropdown menu
    // public Transform dropdownMenu;
    public Dropdown dropDownMenu;
    
    

    private void Awake()
    {
        Start();
    }

    void Start()
    {
        LoadValues();
    }

   
    public void VolumeSlider(float volume)
    {
        volumeTextUI.text = volume.ToString("0.0");
    }

    public void SaveVolume()
    {

        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();

    }
    public void LoadValues()
    {
        LoadDropDown();
        LoadAudioValues();
    }
    void LoadDropDown()
    {
        //get the selected index
        int menuIndex = dropDownMenu.value;

        //get all options available within this dropdown menu
        List<Dropdown.OptionData> menuOptions = dropDownMenu.options;

        //get the string value of the selected index
        string value = menuOptions[menuIndex].text;

        Debug.Log(value);
    }

    void LoadAudioValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        // Set the volume to the slider value
        audioSource.volume = volumeValue;
        if (volumeValue == 0)
        {
            mute.SetActive(true);
            audioOn.SetActive(false);
        }
        else
        {
            audioOn.SetActive(true);
            mute.SetActive(false);
        }

    }

    // if the audio symbol was pressed
    private bool isMuted = false;
    public void Muted()
    {
        if (!isMuted)
        {
            PlayerPrefs.SetFloat("VolumeValue", 0);
            volumeSlider.value = 0;
            audioSource.volume = 0;
            isMuted = true;
        }
        else
        {
            PlayerPrefs.SetFloat("VolumeValue", 1f);
            volumeSlider.value = 1f;
            audioSource.volume = volumeSlider.value;
            isMuted = false;
        }
    }
   
    public void ClosePanel()
    {
        settingPanel.SetActive(false);
    }
}
