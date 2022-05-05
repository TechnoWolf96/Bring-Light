using UnityEngine;

public class Timer_PassiveEffect : PassiveEffect
{
    [HideInInspector] public float duration;

    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0) Destroy(gameObject);
    }

}
