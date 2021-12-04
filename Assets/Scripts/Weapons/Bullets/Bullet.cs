using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Bullet_Parameters
{
    [Header("Universal bullet:")]
    public float speed;                         // �������� ����
    public AttackParameters attack;             // ��������� ����� ����

    [Header("Explosing bullet:")]
    public float radius;                        // ������ ������
    public LayerMask DamagedExplosionLayers;    // ����, ������� ����� �������� ���� �� ������
}



public abstract class Bullet : MonoBehaviour
{
    protected Bullet_Parameters bulletParameters;
    protected Rigidbody2D rb;
    protected Transform target;    // ���� ����� ����
    protected Transform shooter;   // Transform �������

    [SerializeField] protected GameObject deathEffect;        // ������ ������
    [SerializeField] protected GameObject critDeathEffect;    // ������ ������ ��� �����
    [SerializeField] protected float offset;                  // ����������� ������� ������� ������


    // ������� ���������� ���� ��� �� ��������
    public virtual void InstBullet(Bullet_Parameters bulletParameters, Transform shooter, Transform target)
    {
        rb = GetComponent<Rigidbody2D>();
        this.bulletParameters = bulletParameters;
        this.shooter = shooter;
        this.target = target;
        // ������� ����������� � �������� ����
        Vector2 direction = this.target.position - this.shooter.position;
        rb.velocity = direction.normalized * this.bulletParameters.speed;

    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == shooter.gameObject.layer)
            return; // ������������� ����� ��������
        Collision(other);
    }

    protected virtual void Collision(Collider2D other) { }



}
