using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject mob;
    public float recharge;
    public float radius;
    public int quantity;
    public Creature player;

    private void Start()
    {
        StartCoroutine(Spawn(recharge));
    }

    IEnumerator Spawn(float rechange)
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < quantity; i++)
        {
            Vector2 randomPos = new Vector2(transform.position.x + Random.Range(-radius, radius), transform.position.y + Random.Range(-radius, radius));
            Instantiate(mob, randomPos, Quaternion.identity).GetComponent<Stalker>().SetFollow(player.transform);
            yield return new WaitForSeconds(rechange);
        }
    }
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
