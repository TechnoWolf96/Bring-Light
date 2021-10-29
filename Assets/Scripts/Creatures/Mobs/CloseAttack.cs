using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : Stalker
{

    [Header("Close Attack:")]
    [SerializeField] private float offset;
    public GameObject weapon;

    protected override void Update()
    {
        base.Update();
        if (follow != null) Aim(follow);
    }

    protected void Aim(Transform aimPos) // Нацеливание оружия на позицию
    {
        Vector2 difference = aimPos.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }



}
