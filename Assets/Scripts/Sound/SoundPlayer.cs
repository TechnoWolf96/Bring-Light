using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public class SoundPlayer : MonoBehaviour
{
    public List<EventReference> sounds;


    public void PlaySound(int number)
    {
        Library.Play3DSound(sounds[number], transform);
    }
}
