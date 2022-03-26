using UnityEngine;

public abstract class SpellNPC : MonoBehaviour
{
    public int priority{ get; protected set; }
    [SerializeField] protected float speedCast;
    [SerializeField] protected float rechargeTime;
    protected float currentRechargeTime = 0;

    protected virtual void FixedUpdate()
    {
        currentRechargeTime -= Time.deltaTime;
    }

    // Вызывается при начале произношения заклинания
    public abstract void BeginCast();

    // Получить полезный эффект от заклинания (спустя время вызывается в анимации Spellcast)
    public abstract void Activate();

    // Задает правило, по которому вычисляется приоритет заклинания
    public abstract void CalculatePriority();

    public abstract void BreakCast();

}
