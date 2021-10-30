using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject mob;
    public float recharge;
    public float radius;

    private void Start()
    {
        StartCoroutine(spawn(recharge));
    }

    IEnumerator spawn(float rechange)
    {
        while (true)
        {
            Vector2 randomPos = new Vector2(transform.position.x + Random.Range(-radius, radius), transform.position.y + Random.Range(-radius, radius));
            Instantiate(mob, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(rechange);
        }
        
    }
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
