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
        Library.Play2DSound(usingSoundEffect);
        InventoryInfoSlot.singleton.RechargeItemsWithId(GetComponent<Icon>().id);
    }

    private void RechargeItem(int id)
    {
        if (id == GetComponent<Icon>().id)
            currentRechargeTime = rechargeTime;
    }

    protected virtual void Start()
    {
        darkFill = transform.Find("DarkFill").GetComponent<Image>();
        currentRechargeTime = 0f;
        InventoryInfoSlot.singleton.onUseItem += RechargeItem;
    }

    protected virtual void Update()
    {
        darkFill.fillAmount = currentRechargeTime / rechargeTime;
    }

    protected virtual void FixedUpdate()
    {
        currentRechargeTime -= Time.deltaTime;
    }
    protected virtual void OnDestroy()
    {
        InventoryInfoSlot.singleton.onUseItem -= RechargeItem;
    }

}
