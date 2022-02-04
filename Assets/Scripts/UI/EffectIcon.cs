using UnityEngine;
using UnityEngine.UI;

public class EffectIcon : MonoBehaviour
{
    [SerializeField] private int _id;
    public int id { get => _id; }

    protected Image darkFill;

    public float duration { get; set; }
    public float timeToFinish { get; private set; }

    private void Start()
    {
        darkFill = transform.Find("DarkFill").GetComponent<Image>();
        timeToFinish = duration;
    }

    private void FixedUpdate()
    {
        timeToFinish -= Time.deltaTime;
    }
    private void Update()
    {
        darkFill.fillAmount = (duration - timeToFinish) / duration;
        if (timeToFinish < 0f) Destroy(gameObject);
            
    }

}
