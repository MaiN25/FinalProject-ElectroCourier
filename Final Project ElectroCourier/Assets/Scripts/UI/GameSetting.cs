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
        if (PlayerPrefs.GetInt("CaptionToggle") == 1)
        {
            sc.captionBox.SetActive(true);
            captionToggle.isOn = true;
        }
        else if (PlayerPrefs.GetInt("CaptionToggle") == 0)
        {
            sc.captionBox.SetActive(false);
            captionToggle.isOn = false;
        }
        sc.CaptionToText();
        TextSlider.value = PlayerPrefs.GetFloat("Textsize");
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
        LoadAudioValues();

    }

     void LoadTextBoxes()
    {
        if(textTrigger != null)
        {
            textbox = textTrigger.textBoxPrefab;
            TextSlider.value = PlayerPrefs.GetFloat("Textsize");
        }
    }


    public void ChangeTextSize(float size)
    {

        PlayerPrefs.SetFloat("Textsize", size);
        Vector3 scaleChange = new Vector3(size + 0.5f, size + 0.5f, size + 0.5f);
        textTrigger.SetTextBoxSize(scaleChange);
    }


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
        if(captionToggle.isOn == true)
        {
            SoundControl.captionEnabled = 1;
            sc.captionBox.SetActive(true);
        }
        else if (captionToggle.isOn == false)
        {
            SoundControl.captionEnabled = 0;
            sc.captionBox.SetActive(false);
        }
        PlayerPrefs.SetInt("CaptionToggle", SoundControl.captionEnabled);
    }
}
