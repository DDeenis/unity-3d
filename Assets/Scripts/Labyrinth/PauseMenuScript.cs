using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    Slider musicVolumeSlider;
    [SerializeField]
    Slider effectsVolumeSlider;
    [SerializeField]
    Toggle muteAllToggle;
    [SerializeField]
    GameObject content;
    [SerializeField]
    AudioMixer soundMixer;
    [SerializeField]
    TMPro.TMP_Dropdown qualityDropdown;

    // Start is called before the first frame update
    void Start()
    {
        string[] names = QualitySettings.names;
        if (names.Length != qualityDropdown.options.Count)
        {
            qualityDropdown.options.Clear();
            qualityDropdown.options.AddRange(names.Select(n => new TMPro.TMP_Dropdown.OptionData(n)));
            qualityDropdown.value = QualitySettings.GetQualityLevel();
        }
        else
        {
            OnQualityDropdownChanged(qualityDropdown.value);
        }

        OnMusicVolumeChange(musicVolumeSlider.value);
        OnEffectsVolumeChange(effectsVolumeSlider.value);
        LabyrinthState.isSoundMuted = muteAllToggle.isOn;

        if (content.activeInHierarchy)
        {
            ShowMenu();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (content.activeInHierarchy)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }
    }

    public void OnMusicVolumeChange(float volume)
    {
        LabyrinthState.musicVolume = volume;
        if (!LabyrinthState.isSoundMuted)
        {
            float dB = -80f + 90f * volume;
            soundMixer.SetFloat("MusicVolume", dB);
        }
    }

    public void OnEffectsVolumeChange(float volume)
    {
        LabyrinthState.effectsVolume = volume;
        if (!LabyrinthState.isSoundMuted)
        {
            float dB = -80f + 90f * volume;
            soundMixer.SetFloat("EffectsVolume", dB);
        }
    }

    public void OnMuteAllChanged(bool value)
    {
        LabyrinthState.isSoundMuted = value;
        if (value)
        {
            soundMixer.SetFloat("MusicVolume", -80f);
            soundMixer.SetFloat("EffectsVolume", -80f);
        }
        else
        {
            OnMusicVolumeChange(LabyrinthState.musicVolume);
            OnEffectsVolumeChange(LabyrinthState.effectsVolume);
        }
    }

    void ShowMenu()
    {
        content.SetActive(true);
        Time.timeScale = 0f;
        LabyrinthState.isPaused = true;
    }

    void HideMenu()
    {
        content.SetActive(false);
        Time.timeScale = 1f;
        LabyrinthState.isPaused = false;
    }

    public void OnExitButtonClick()
    {
        if (Application.isEditor)
        {
            // EditorApplication.Exit(0);
            EditorApplication.ExitPlaymode();
        }
        else
        {
            Application.Quit(0);
        }
    }
    public void OnResetButtonClick()
    {
        OnMusicVolumeChange(0.5f);
        OnEffectsVolumeChange(0.5f);
        OnMuteAllChanged(false);
    }
    public void OnCloseButtonClick()
    {
        HideMenu();
    }

    public void OnQualityDropdownChanged(int value)
    {
        QualitySettings.SetQualityLevel(value, true);
    }
}
