using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Usingable : MonoBehaviour
{
    [Header("Usingable:")]
    public float rechargeTime;
    public Image darkFill;

    protected float currentRechargeTime = 0;

    protected virtual void Start()
    {
    }

    private void FixedUpdate()
    {
        currentRechargeTime -= Time.deltaTime;
        darkFill.fillAmount = currentRechargeTime / rechargeTime;
    }

    public virtual bool UseItem() // Возвращает true, если предмет удалось использовать
    {
        if (currentRechargeTime > 0) return false;
        currentRechargeTime = rechargeTime;
        return true;
    }

}
