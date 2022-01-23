using UnityEngine;

public interface IBullet_Parameters{}

public abstract class Bullet : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Transform shotPoint;   // ����� ��������

    [SerializeField] protected GameObject deathEffect;        // ������ ������
    [SerializeField] protected GameObject critDeathEffect;    // ������ ������ ��� �����
    [SerializeField] protected float offset;                  // ����������� ������� ������� ������


    // ������� ���������� ���� ��� �� ��������
    public virtual void InstBullet(IBullet_Parameters bulletParameters, Transform shotPoint, Transform target)
    {
        rb = GetComponent<Rigidbody2D>();
        this.shotPoint = shotPoint;
    }

    protected void SetDirectionAndSpeed(Transform targetPosition, float speed)
    {
        // ������� ����������� � �������� ����
        Vector2 direction = targetPosition.position - shotPoint.position;
        rb.velocity = direction.normalized * speed;
        // ������� ���� � ������� ����
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
            Mathf.Atan2(targetPosition.position.y - transform.position.y, targetPosition.position.x - transform.position.x) * Mathf.Rad2Deg - 180);
    }


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == shotPoint.GetComponentInParent<Transform>().gameObject.layer)
            return; // ������������� ����� ��������
        Collision(other);
    }

    protected abstract void Collision(Collider2D other);
    protected virtual void CritEffect() { }



}
