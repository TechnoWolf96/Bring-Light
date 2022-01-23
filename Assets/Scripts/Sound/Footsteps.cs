using UnityEngine;
using FMODUnity;


public enum MaterialSound
{
    Stone, Wood, Gravel, Water
}



public class Footsteps : MonoBehaviour
{
    [SerializeField] private EventReference footstepSound;
    public MaterialSound currentMaterial;

    public void PlayFootstep()
    {
        FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(footstepSound);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        SetAllParametersNull(instance);
        instance.setParameterByName(currentMaterial.ToString(), 1f);
        instance.start();
    }

    private void SetAllParametersNull(FMOD.Studio.EventInstance instance)
    {
        instance.setParameterByName("Stone", 0);
        instance.setParameterByName("Wood", 0);
        instance.setParameterByName("Gravel", 0);
        instance.setParameterByName("Water", 0);
    }


}
