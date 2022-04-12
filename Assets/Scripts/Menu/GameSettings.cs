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
    public static GameSettings singleton { get; private set; }

    [Header("Settings elements:")]
    [SerializeField] Button applyGameSettingsButton;
    [SerializeField] Button _applyControlButton;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider brightnessSlider;
    [SerializeField] PostProcessProfile postProcessing;

    public Button applyControlButton { get => _applyControlButton; }


    const float defaultSoundVolume = 0.7f;
    const float defaultMusicVolume = 0.7f;
    const float defaultBrightness = 0f;

    const KeyCode defaultConsumable1 = KeyCode.Alpha1;
    const KeyCode defaultConsumable2 = KeyCode.Alpha2;
    const KeyCode defaultConsumable3 = KeyCode.Alpha3;
    const KeyCode defaultConsumable4 = KeyCode.Alpha4;
    const KeyCode defaultConsumable5 = KeyCode.Alpha5;
    const KeyCode defaultRangedAttack = KeyCode.Mouse0;
    const KeyCode defaultCloseAttack = KeyCode.Mouse1;
    const KeyCode defaultOpenInventory = KeyCode.E;
    const KeyCode defaultTakeItem = KeyCode.F;
    const KeyCode defaultChangeArrows = KeyCode.Q;




    FMOD.Studio.Bus sound;
    FMOD.Studio.Bus music;



    private void Awake()
    {
        singleton = this;
        sound = RuntimeManager.GetBus("bus:/Master/SFX");
        music = RuntimeManager.GetBus("bus:/Master/Music");
        
        
    }
    private void Start()
    {
        LoadGameplaySettings();
        LoadControlSettings();
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

        applyGameSettingsButton.interactable = false;
    }

    public void LoadControlSettings()
    {
        //==================================================
        if (PlayerPrefs.HasKey("Button_Consumable1")) Control.singleton.Consumable1 = (KeyCode)PlayerPrefs.GetInt("Button_Consumable1");
        else Control.singleton.Consumable1 = defaultConsumable1;
        //==================================================
        if (PlayerPrefs.HasKey("Button_Consumable2")) Control.singleton.Consumable2 = (KeyCode)PlayerPrefs.GetInt("Button_Consumable2");
        else Control.singleton.Consumable2 = defaultConsumable2;
        //==================================================
        if (PlayerPrefs.HasKey("Button_Consumable3")) Control.singleton.Consumable3 = (KeyCode)PlayerPrefs.GetInt("Button_Consumable3");
        else Control.singleton.Consumable3 = defaultConsumable3;
        //==================================================
        if (PlayerPrefs.HasKey("Button_Consumable4")) Control.singleton.Consumable4 = (KeyCode)PlayerPrefs.GetInt("Button_Consumable4");
        else Control.singleton.Consumable4 = defaultConsumable4;
        //==================================================
        if (PlayerPrefs.HasKey("Button_Consumable5")) Control.singleton.Consumable5 = (KeyCode)PlayerPrefs.GetInt("Button_Consumable5");
        else Control.singleton.Consumable5 = defaultConsumable5;
        //==================================================
        if (PlayerPrefs.HasKey("Button_RangedAttack")) Control.singleton.RangedAttack = (KeyCode)PlayerPrefs.GetInt("Button_RangedAttack");
        else Control.singleton.RangedAttack = defaultRangedAttack;
        //==================================================
        if (PlayerPrefs.HasKey("Button_CloseAttack")) Control.singleton.CloseAttack = (KeyCode)PlayerPrefs.GetInt("Button_CloseAttack");
        else Control.singleton.CloseAttack = defaultCloseAttack;
        //==================================================
        if (PlayerPrefs.HasKey("Button_ChangeArrows")) Control.singleton.ChangeArrows = (KeyCode)PlayerPrefs.GetInt("Button_ChangeArrows");
        else Control.singleton.ChangeArrows = defaultChangeArrows;
        //==================================================
        if (PlayerPrefs.HasKey("Button_OpenInventory")) Control.singleton.OpenInventory = (KeyCode)PlayerPrefs.GetInt("Button_OpenInventory");
        else Control.singleton.OpenInventory = defaultOpenInventory;
        //==================================================
        if (PlayerPrefs.HasKey("Button_TakeItem")) Control.singleton.TakeItem = (KeyCode)PlayerPrefs.GetInt("Button_TakeItem");
        else Control.singleton.TakeItem = defaultTakeItem;

        _applyControlButton.interactable = false;
    }



    public void SetActiveControlApplyButton()
    {
        _applyControlButton.interactable = true;
    }

    public void ApplyGameSettingsChanges()
    {
        
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
        applyGameSettingsButton.interactable = false;
    }

    public void ApplyControlChanges()
    {
        PlayerPrefs.SetInt("Button_Consumable1", (int)Control.singleton.Consumable1);
        PlayerPrefs.SetInt("Button_Consumable2", (int)Control.singleton.Consumable2);
        PlayerPrefs.SetInt("Button_Consumable3", (int)Control.singleton.Consumable3);
        PlayerPrefs.SetInt("Button_Consumable4", (int)Control.singleton.Consumable4);
        PlayerPrefs.SetInt("Button_Consumable5", (int)Control.singleton.Consumable5);
        PlayerPrefs.SetInt("Button_RangedAttack", (int)Control.singleton.RangedAttack);
        PlayerPrefs.SetInt("Button_CloseAttack", (int)Control.singleton.CloseAttack);
        PlayerPrefs.SetInt("Button_OpenInventory", (int)Control.singleton.OpenInventory);
        PlayerPrefs.SetInt("Button_ChangeArrows", (int)Control.singleton.ChangeArrows);
        PlayerPrefs.SetInt("Button_TakeItem", (int)Control.singleton.TakeItem);
        _applyControlButton.interactable = false;
    }


    public void BackGameplaySettings()
    {
        LoadGameplaySettings();
        SettingsPanel.singleton.Close();
        MenuPanel.singleton.Open();
    }

    public void BackFromControl()
    {
        LoadControlSettings();
        ControlPanel.singleton.Close();
        SettingsPanel.singleton.Open();
    }



    public void SetSoundVolume()
    {
        sound.setVolume(soundSlider.value);
        applyGameSettingsButton.interactable = true;
    }

    public void SetMusicVolume()
    {
        music.setVolume(musicSlider.value);
        applyGameSettingsButton.interactable = true;
    }

    public void SetBrightness()
    {
        AutoExposure brightness = postProcessing.GetSetting<AutoExposure>();
        brightness.maxLuminance.value = -brightnessSlider.value;
        brightness.minLuminance.value = -brightnessSlider.value;
        applyGameSettingsButton.interactable = true;
    }


    public void SetResolution(int index)
    {
        
    }



}
