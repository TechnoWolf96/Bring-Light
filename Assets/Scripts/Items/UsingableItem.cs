using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UsingableItem : MonoBehaviour, IUsingable
{
    public float rechargeTime;
    public Image darkFill;
    [SerializeField] protected EventReference usingSoundEffect;

    protected Player player;
    protected float currentRechargeTime;
    
    public bool IsRecharged()
    {
        return currentRechargeTime < 0;
    }

    public virtual void Use()
    {
        currentRechargeTime = rechargeTime;
        Library.Play2DSound(usingSoundEffect);
    }

    protected virtual void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        currentRechargeTime = 0f;
    }

    protected virtual void Update()
    {
        darkFill.fillAmount = currentRechargeTime / rechargeTime;
    }

    protected virtual void FixedUpdate()
    {
        currentRechargeTime -= Time.deltaTime;
    }

}
