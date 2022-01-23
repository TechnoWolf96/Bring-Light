using UnityEngine;
public interface IDestructable
{
    public void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null);
    public void Death();
}
