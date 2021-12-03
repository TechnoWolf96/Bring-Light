using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ExplosingBullet_Parameters
{
    public float speed;                         // �������� ����
    public AttackParameters attack;             // ��������� ����� ����
    [HideInInspector] public Transform carrier; // Transform �������� ������, �� �������� �������� ����
    public LayerMask layer;                     // ���� ��������, �� �������� ����� ��������� �����
    [HideInInspector] public Transform target;  // ���� ����� ����
    public float radius;                        // ������ ������
}

public class ExplosingBullet : MonoBehaviour
{
    private ExplosingBullet_Parameters bulletParameters;
    private Rigidbody2D rb;
    [SerializeField] private GameObject explotionEffect;
    [SerializeField] private GameObject critExplotionEffect;
    [SerializeField] private float offset;

    public virtual void InstBullet(ExplosingBullet_Parameters bulletParameters)
    {
        rb = GetComponent<Rigidbody2D>();
        this.bulletParameters = bulletParameters;
        // ������� ����������� � �������� ����
        Vector2 difference = this.bulletParameters.target.position - this.bulletParameters.carrier.position;
        rb.velocity = difference.normalized * this.bulletParameters.speed;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Wall")) Explotion();

    }


   
    protected virtual void Explotion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bulletParameters.radius, bulletParameters.layer);
        bool crit = bulletParameters.attack.SetCrit();
        foreach (var item in colliders)
        {
            item.GetComponent<Creature>().GetDamage(bulletParameters.attack, bulletParameters.carrier.transform, transform);
        }
        if (!crit) Instantiate(explotionEffect, transform.position, Quaternion.identity).transform.localScale *= bulletParameters.radius * offset;
        Destroy(gameObject);
    }







}
