using FunkyCode;
using UnityEngine;

public class ShutdownShadow : MonoBehaviour
{
    private LightCollider2D lightCollider;

    private void Start()
    {
        GetComponentInParent<Creature>().onDeath += DisableShadow;
    }

    private void DisableShadow()
    {
        lightCollider.enabled = false;
    }
}
