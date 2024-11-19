using UnityEngine;
using UnityEngine.UI;

public class SettingsUIController : MonoBehaviour
{
    public GameSettings gameSettings;

    // UI Elements
    [Header("Gameplay Settings UI")]
    public Slider mouseSensitivitySlider;
    public Slider fieldOfViewSlider;

    [Header("Graphics Settings UI")]
    public Dropdown qualityLevelDropdown;
    public Toggle fullscreenToggle;
    public Dropdown refreshRateDropdown;

    [Header("Audio Settings UI")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    void Start()
    {
        // Load current settings into the UI
        InitializeUI();
    }

    void InitializeUI()
    {
        // Gameplay Settings
        mouseSensitivitySlider.value = gameSettings.mouseSensitivity;
        fieldOfViewSlider.value = gameSettings.fieldOfView;

        // Graphics Settings
        qualityLevelDropdown.value = (int)gameSettings.qualityLevel;
        fullscreenToggle.isOn = gameSettings.isFullscreen;
        refreshRateDropdown.value = GetRefreshRateIndex(gameSettings.refreshRate);

        // Audio Settings
        masterVolumeSlider.value = gameSettings.masterVolume;
        musicVolumeSlider.value = gameSettings.musicVolume;
        sfxVolumeSlider.value = gameSettings.sfxVolume;
    }

    int GetRefreshRateIndex(GameSettings.RefreshRate refreshRate)
    {
        switch (refreshRate)
        {
            case GameSettings.RefreshRate.Hz60: return 0;
            case GameSettings.RefreshRate.Hz90: return 1;
            case GameSettings.RefreshRate.Hz120: return 2;
            case GameSettings.RefreshRate.Unlimited: return 3;
            default: return 0;
        }
    }

    public void OnMouseSensitivityChanged(float value)
    {
        gameSettings.mouseSensitivity = value;
    }

    public void OnFieldOfViewChanged(float value)
    {
        gameSettings.fieldOfView = value;
    }

    public void OnQualityLevelChanged(int index)
    {
        gameSettings.qualityLevel = (GameSettings.QualityLevel)index;
    }

    public void OnFullscreenToggleChanged(bool isOn)
    {
        gameSettings.isFullscreen = isOn;
    }

    public void OnRefreshRateChanged(int index)
    {
        GameSettings.RefreshRate[] rates = {
            GameSettings.RefreshRate.Hz60,
            GameSettings.RefreshRate.Hz90,
            GameSettings.RefreshRate.Hz120,
            GameSettings.RefreshRate.Unlimited
        };
        gameSettings.refreshRate = rates[index];
    }

    public void OnMasterVolumeChanged(float value)
    {
        gameSettings.masterVolume = value;
    }

    public void OnMusicVolumeChanged(float value)
    {
        gameSettings.musicVolume = value;
    }

    public void OnSFXVolumeChanged(float value)
    {
        gameSettings.sfxVolume = value;
    }

    public void OnApplyButtonPressed()
    {
        // Apply the new settings
        SettingsManager settingsManager = FindObjectOfType<SettingsManager>();
        settingsManager.ApplyAllSettings();

        // Save the settings
        SettingsSaver.SaveSettings(gameSettings);
    }
}
