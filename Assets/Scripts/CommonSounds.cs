using FMODUnity;
using UnityEngine;

public class CommonSounds : MonoBehaviour
{
    public static CommonSounds singleton { get; private set; }

    public EventReference takeItem;




    private void Awake()
    {
        singleton = this;
    }


}
