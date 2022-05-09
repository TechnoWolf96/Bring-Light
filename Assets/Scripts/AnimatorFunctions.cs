using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
    public void DestroyObject() => Destroy(gameObject);

    public void StopParticles() => GetComponent<ParticleSystem>().Stop();

}
