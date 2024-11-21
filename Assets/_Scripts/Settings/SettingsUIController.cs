using UnityEngine;
using UnityEngine.UI;

public class SettingsUIController : MonoBehaviour
{
    public GameSettings gameSettings;
    private SettingsManager settingsManager;

    // UI Elements
    [Header("Gameplay Settings UI")]
    public Slider mouseSensitivitySlider;
    public Text mouseSensitivityText;
    public Slider fieldOfViewSlider;
    public Text fieldOfViewText;

    [Header("Graphics Settings UI")]
    public Dropdown qualityLevelDropdown;
    public Toggle fullscreenToggle;
    public Dropdown refreshRateDropdown;

    [Header("Audio Settings UI")]
    public Slider masterVolumeSlider;
    public Text masterVolumeText;
    public Slider musicVolumeSlider;
    public Text musicVolumeText;
    public Slider sfxVolumeSlider;
    public Text sfxVolumeText;

    void OnEnable()
    {
        // Get the SettingsManager instance
        settingsManager = FindObjectOfType<SettingsManager>();

        if (settingsManager != null)
        {
            // Reference the gameSettings from SettingsManager
            gameSettings = settingsManager.gameSettings;

            // Load saved settings into gameSettings
            SettingsSaver.LoadSettings(gameSettings);

            // Initialize the UI with the loaded settings
            InitializeUI();
        }
        else
        {
            Debug.LogError("SettingsManager not found in the scene.");
        }
    }

    void Start()
    {
        // Load current settings into the UI
        InitializeUI();
    }

    void InitializeUI()
    {
        // Gameplay Settings
        mouseSensitivitySlider.value = gameSettings.mouseSensitivity;
        mouseSensitivityText.text = gameSettings.mouseSensitivity.ToString("F1");

        fieldOfViewSlider.value = gameSettings.fieldOfView;
        fieldOfViewText.text = gameSettings.fieldOfView.ToString("F0");

        // Graphics Settings
        qualityLevelDropdown.value = (int)gameSettings.qualityLevel;
        fullscreenToggle.isOn = gameSettings.isFullscreen;
        refreshRateDropdown.value = GetRefreshRateIndex(gameSettings.refreshRate);

        // Audio Settings
        masterVolumeSlider.value = gameSettings.masterVolume;
        masterVolumeText.text = (gameSettings.masterVolume * 100f).ToString("F0") + "%";

        musicVolumeSlider.value = gameSettings.musicVolume;
        musicVolumeText.text = (gameSettings.musicVolume * 100f).ToString("F0") + "%";

        sfxVolumeSlider.value = gameSettings.sfxVolume;
        sfxVolumeText.text = (gameSettings.sfxVolume * 100f).ToString("F0") + "%";
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
        mouseSensitivityText.text = value.ToString("F1");
    }

    public void OnFieldOfViewChanged(float value)
    {
        gameSettings.fieldOfView = value;
        fieldOfViewText.text = value.ToString("F0");
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
        masterVolumeText.text = (value * 100f).ToString("F0") + "%";
    }

    public void OnMusicVolumeChanged(float value)
    {
        gameSettings.musicVolume = value;
        musicVolumeText.text = (value * 100f).ToString("F0") + "%";
    }

    public void OnSFXVolumeChanged(float value)
    {
        gameSettings.sfxVolume = value;
        sfxVolumeText.text = (value * 100f).ToString("F0") + "%";
    }

    public void OnApplyButtonPressed()
    {
        if (settingsManager != null)
        {
            // Apply the new settings
            settingsManager.ApplyAllSettings();

            // Save the settings
            SettingsSaver.SaveSettings(gameSettings);
        }
        else
        {
            Debug.LogError("SettingsManager is not assigned.");
        }
    }
}
