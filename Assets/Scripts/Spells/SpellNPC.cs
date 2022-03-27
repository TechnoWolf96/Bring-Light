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

    // ���������� ��� ������ ������������ ����������
    public virtual void BeginCast()
    {
        if (startParticles != null)
            instParticles = Instantiate(startParticles, transform).GetComponent<ParticleSystem>();
        spellcaster.anim.SetFloat("SpeedCast", speedCast);
        currentRechargeTime = rechargeTime;
    }

    // �������� �������� ������ �� ���������� (������ ����� ���������� � �������� Spellcast)
    public abstract void Activate();

    // ������ �������, �� �������� ����������� ��������� ����������
    public abstract void CalculatePriority();

    public virtual void BreakCast() => instParticles?.Stop();

}
