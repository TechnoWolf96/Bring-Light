using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject unit;
    [SerializeField] private float timeCicleSpawn;
    private float untilSpawn;

    private void Start()
    {
        untilSpawn = timeCicleSpawn;
    }
    private void FixedUpdate()
    {
        untilSpawn -= Time.deltaTime;
    }
    private void Update()
    {
        if (untilSpawn < 0)
        {
            untilSpawn = timeCicleSpawn;
            Instantiate(unit, transform.position, Quaternion.identity).
                GetComponent<CloseAttackFSM>().follow = Player.singleton;
        }
    }

}
