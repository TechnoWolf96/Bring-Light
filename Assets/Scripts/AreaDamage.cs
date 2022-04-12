using System.Collections;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    [SerializeField] private int damagePerCicle;
    [SerializeField] private float cicleTime;
    [SerializeField] private GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out Creature creature))
        {
            var damageEffect = Instantiate(effect, creature.transform).GetComponent<ContinuousDamage_Effect>();
            damageEffect.damagePerCicle = damagePerCicle;
            damageEffect.cicleTime = cicleTime;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Нужно будет поменять
        foreach (Transform child in collision.transform.parent)
        {
            if (child.tag == "Effect") StartCoroutine(WaitUntilDestroy(1f, child.gameObject));
        }
    }

    IEnumerator WaitUntilDestroy(float time, GameObject gameObject)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
