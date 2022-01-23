using UnityEngine;
using FMODUnity;

public class MainMusic : MonoBehaviour
{
    [SerializeField] protected EventReference currentMusic;
    FMOD.Studio.EventInstance instance;

    private void Start()
    {
        instance = RuntimeManager.CreateInstance(currentMusic);
        instance.start();
    }

    public void ChangeMusic(EventReference newMusic)
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        currentMusic = newMusic;
        instance = RuntimeManager.CreateInstance(currentMusic);
        instance.start();
    }


}
