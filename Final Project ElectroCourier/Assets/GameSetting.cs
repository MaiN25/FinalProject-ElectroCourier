using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DigitalRuby.RainMaker;
using System;

public class GameSetting : MonoBehaviour
{
    public static GameSetting instance = null;
    public GameObject settingPanel;
    public static float volumeValue;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private TMP_Text volumeTextUI = null;
    [SerializeField] public GameObject mute;
    [SerializeField] public GameObject audioOn;
    
    //Audio
    AudioSource audioSource;
    private SoundControl sc;
    
    //TextSize
    private TempTrigger textTrigger;
    [SerializeField] private Slider TextSlider = null;
    private GameObject textbox;


    //Screen Resolution
    public GameObject dropDownObject;
    TMP_Dropdown dropDownMenu;
    Resolution[] resolutions;

    //Caption Toggle
    public Toggle captionToggle;

    private void Awake()
    {
        sc = GameObject.FindObjectOfType<SoundControl>();
        textTrigger = GameObject.FindObjectOfType<TempTrigger>();
        audioSource = sc.singletonAudio;
        PlayerPrefs.SetInt("CaptionToggle", SoundControl.captionEnabled);
        sc.captionBox = GameObject.Find("CaptionBox");
        sc.captionText = sc.captionBox.GetComponentInChildren<TextMeshProUGUI>();
        ToggleCaptions();
        sc.CaptionToText();
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
        LoadTextBoxes();
        LoadDropDown();
        LoadAudioValues();

    }

     void LoadTextBoxes()
    {
        if(textTrigger != null)
        {
            textbox = textTrigger.textBoxPrefab;
        }
    }


    public void ChangeTextSize(float size)
    {
        Vector3 scaleChange = new Vector3(size+0.5f, size + 0.5f, size + 0.5f);
        textbox.transform.localScale = scaleChange;
    }

#if UNITY_WEBGL

    void LoadDropDown()
    {
        //Get the UI dropdown component
        dropDownMenu = dropDownObject.GetComponent<TMP_Dropdown>();

        //get the selected index
        //int defaultmenuIndex = 0;

        //get all options available within this dropdown menu
        List<TMP_Dropdown.OptionData> menuOptions = dropDownMenu.options;

        //get the string value of the selected index
        //string value = menuOptions[defaultmenuIndex].text;


        /*
        resolutions = Screen.resolutions;
        dropDownMenu.ClearOptions();
        List<string> menuOptions = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string menuOption = resolutions[i].width + " × " + resolutions[i].height;
            menuOptions.Add(menuOption);
            if(resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        dropDownMenu.AddOptions(menuOptions);
        dropDownMenu.value = currentResolutionIndex;
        dropDownMenu.RefreshShownValue();
        */

    }

    public void ChangeDropDownSelection(int menuIndex)
    {

        //get all options available within this dropdown menu
        List<TMP_Dropdown.OptionData> menuOptions = dropDownMenu.options;

        //get the string value of the selected index
        string value = menuOptions[menuIndex].text;

        int index = value.IndexOf(' ');
        string ScreenWidth = value.Substring(0, index);
        string ScreenHeight = value.Substring(index + 2);
        // Resolution resolution = resolutions[menuIndex];
        Screen.SetResolution(int.Parse(ScreenWidth), int.Parse(ScreenHeight), Screen.fullScreen);
        Debug.Log(ScreenWidth +" "+ ScreenHeight);
    }
#endif
    void LoadAudioValues()
    {
        volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        
        // Set the volume to the slider value
        audioSource.volume = volumeValue * 0.8f;
        LoopingAudioSource.TargetVolume = volumeValue * 1.2f;
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

    public void ToggleCaptions()
    {
        if(captionToggle.isOn == true || (PlayerPrefs.GetInt("CaptionToggle") == 1))
        {
            SoundControl.captionEnabled = 1;
            sc.captionBox.SetActive(true);
            //sc.StartCoroutine("ReduceCaptionList", 5f);
        }
        else if (captionToggle.isOn == false || (PlayerPrefs.GetInt("CaptionToggle") == 0))
        {
            SoundControl.captionEnabled = 0;
            sc.captionBox.SetActive(false);
            //sc.StopCoroutine("ReduceCaptionList");
        }
    }
}
