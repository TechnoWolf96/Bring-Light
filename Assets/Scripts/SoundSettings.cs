using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class SoundSettings : MonoBehaviour
{
    //FMOD.Studio.Bus master;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus music;

    public Slider SFXSlider;
    public Slider musicSlider;


    private void Awake()
    {
        //master = RuntimeManager.GetBus("bus:/Master Bus");
        SFX = RuntimeManager.GetBus("bus:/Master/SFX");
        music = RuntimeManager.GetBus("bus:/Master/Music");
        SFX.setVolume(SFXSlider.value);
        music.setVolume(musicSlider.value);
    }

    public void SFXSetVolume()
    {
        SFX.setVolume(SFXSlider.value);
    }

    public void MusicSetVolume()
    {
        music.setVolume(musicSlider.value);
    }


}
