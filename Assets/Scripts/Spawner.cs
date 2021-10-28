using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject mob;
    public float rechange;
    public float radius;

    private void Start()
    {
        StartCoroutine(spawn(rechange));
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

}
