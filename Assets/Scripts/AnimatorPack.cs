using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPack : MonoBehaviour
{
    public List<GameObject> particles;
    public List<Transform> posParticles;


    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void InstParticle(int index)
    {
        Instantiate(particles[index], posParticles[index]);
    }


}
