using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public abstract class UsingableItem : MonoBehaviour, IUsingable
{
    [SerializeField] protected float rechargeTime;
    [SerializeField] protected EventReference usingSoundEffect;
    protected Image darkFill;
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

    protected virtual void Start()
    {
        darkFill = transform.Find("DarkFill").GetComponent<Image>();
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
