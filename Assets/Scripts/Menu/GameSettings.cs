using FMODUnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

enum Resolution
{
    R800x600, R1920x1080, R2560x1440
}





public class GameSettings : MonoBehaviour
{
    [Header("Settings elements:")]
    [SerializeField] Button applyButton;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider brightnessSlider;
    [SerializeField] Dropdown qualityDropdown;
    [SerializeField] PostProcessProfile postProcessing;



    const float defaultSoundVolume = 0.7f;
    const float defaultMusicVolume = 0.7f;
    const float defaultBrightness = 0f;
    const int defaultQuality = 1;



    
    FMOD.Studio.Bus sound;
    FMOD.Studio.Bus music;



    private void Awake()
    {
        sound = RuntimeManager.GetBus("bus:/Master/SFX");
        music = RuntimeManager.GetBus("bus:/Master/Music");
        LoadGameplaySettings();
        
    }

    public void LoadGameplaySettings()
    {
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
            sound.setVolume(PlayerPrefs.GetFloat("SoundVolume"));
        }
        else
        {
            soundSlider.value = defaultSoundVolume;
            sound.setVolume(defaultSoundVolume);
        }
        //==================================================
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            music.setVolume(PlayerPrefs.GetFloat("MusicVolume"));
        }
        else
        {
            musicSlider.value = defaultMusicVolume;
            music.setVolume(defaultMusicVolume);
        }
        //==================================================
        if (PlayerPrefs.HasKey("Brightness"))
        {
            brightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
            SetBrightness();
        }
        else
        {
            brightnessSlider.value = defaultBrightness;
            SetBrightness();
        }
        //==================================================
        if (PlayerPrefs.HasKey("Quality"))
        {
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(qualityDropdown.value);
        }
        else
        {
            qualityDropdown.value = defaultQuality;
            QualitySettings.SetQualityLevel(qualityDropdown.value);
        }



        applyButton.interactable = false;
    }




    public void ApplyChanges()
    {
        applyButton.interactable = false;
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
        PlayerPrefs.SetInt("Quality", qualityDropdown.value);
    }
    public void Back()
    {
        LoadGameplaySettings();
        SettingsPanel.singleton.Close();
        MenuPanel.singleton.Open();
    }

    public void BackFromControl()
    {
        ControlPanel.singleton.Close();
        SettingsPanel.singleton.Open();
    }



    public void SetSoundVolume()
    {
        sound.setVolume(soundSlider.value);
        applyButton.interactable = true;
    }

    public void SetMusicVolume()
    {
        music.setVolume(musicSlider.value);
        applyButton.interactable = true;
    }

    public void SetBrightness()
    {
        AutoExposure brightness = postProcessing.GetSetting<AutoExposure>();
        brightness.maxLuminance.value = -brightnessSlider.value;
        brightness.minLuminance.value = -brightnessSlider.value;
        applyButton.interactable = true;
    }


    public void SetResolution(int index)
    {
        
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(qualityDropdown.value);
        applyButton.interactable = true;
    }



}
