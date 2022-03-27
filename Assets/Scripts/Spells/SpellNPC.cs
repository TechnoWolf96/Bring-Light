using UnityEngine;

public abstract class SpellNPC : MonoBehaviour
{
    public int priority{ get; protected set; }
    [SerializeField] protected float speedCast;
    [SerializeField] protected float rechargeTime;
    [SerializeField] protected GameObject startParticles;
    protected ParticleSystem instParticles;
    protected float currentRechargeTime = 0;
    protected CloseAttackFSM spellcaster;

    protected virtual void Start()
    {
        spellcaster = GetComponentInParent<CloseAttackFSM>();
    }

    protected virtual void FixedUpdate()
    {
        currentRechargeTime -= Time.deltaTime;
    }

    // Вызывается при начале произношения заклинания
    public virtual void BeginCast()
    {
        if (startParticles != null)
            instParticles = Instantiate(startParticles, transform).GetComponent<ParticleSystem>();
        spellcaster.anim.SetFloat("SpeedCast", speedCast);
        currentRechargeTime = rechargeTime;
    }

    // Получить полезный эффект от заклинания (спустя время вызывается в анимации Spellcast)
    public abstract void Activate();

    // Задает правило, по которому вычисляется приоритет заклинания
    public abstract void CalculatePriority();

    public virtual void BreakCast() => instParticles?.Stop();

}
