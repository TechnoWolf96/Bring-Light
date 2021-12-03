using UnityEngine;


[System.Serializable]
public struct OneBullet_Parameters
{
    public float speed;                         // �������� ����
    public AttackParameters attack;             // ��������� ����� ����
    [HideInInspector] public Transform carrier; // Transform �������� ������, �� �������� �������� ����
    public LayerMask collisionLayer;             // ���� ��������, � �������� ����� ������������ ����
    [HideInInspector] public Transform target;  // ���� ����� ����
}

// ����, ���������� ���� ����
public class OneBullet : MonoBehaviour
{
    private OneBullet_Parameters bulletParameters;
    private Rigidbody2D rb;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject critDeathEffect;
    [SerializeField] private float offset;
    [SerializeField] private float radiusDetect;   // ������ ��������� ������ ������� ����������
    public virtual void InstBullet(OneBullet_Parameters bulletParameters)
    {
        rb = GetComponent<Rigidbody2D>();
        this.bulletParameters = bulletParameters;
        this.bulletParameters.collisionLayer = bulletParameters.collisionLayer;
        // ������� ����������� � �������� ����
        Vector2 direction = this.bulletParameters.target.position - this.bulletParameters.carrier.position;
        rb.velocity = direction.normalized * this.bulletParameters.speed;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, radiusDetect, bulletParameters.collisionLayer);
        if (collision != null)
        {
            Damage(collision);
            Destroy(gameObject);
        }
    }


    protected virtual void Damage(Collider2D collider)
    {
        bool crit = bulletParameters.attack.SetCrit();
        collider.GetComponent<Creature>()?.GetDamage(bulletParameters.attack, bulletParameters.carrier, transform);
        if (!crit) Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
        else Instantiate(critDeathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
    }


    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusDetect);
    }



}
