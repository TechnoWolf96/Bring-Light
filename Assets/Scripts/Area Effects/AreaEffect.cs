using UnityEngine;

public abstract class AreaEffect : MonoBehaviour
{
    [SerializeField] protected string effectTag;
    [SerializeField] protected GameObject effect;
    [SerializeField] protected LayerMask layerMask;
    protected GameObject instEffect;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out Creature creature)
            && Library.CompareLayer(collision.transform.parent.gameObject.layer, layerMask))
            instEffect = Instantiate(effect, creature.transform);
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        foreach (Transform child in collision.transform.parent)
        {
            if (child.tag == effectTag)
            {
                child.GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}
