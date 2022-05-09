using UnityEngine;

public class AnimTimer_PassiveEffect : PassiveEffect
{
    [HideInInspector] public float lifeTime;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) _animator.SetTrigger("End");
    }
}
