using UnityEngine;

public class PlacedEffect_UsingableItem : UsingableItem
{
    [SerializeField] private GameObject _placedEffect;
    [SerializeField] private float _lifeTime;


    public override void Use()
    {
        base.Use();
        Instantiate(_placedEffect, Player.singleton.transform.position, Quaternion.identity).
            GetComponent<AnimTimer_PassiveEffect>().lifeTime = _lifeTime;
    }

}
