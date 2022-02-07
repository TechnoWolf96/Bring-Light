using UnityEngine;

public class Effect_Timer : MonoBehaviour
{
    public float duration { get; set; }

    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0) Destroy(gameObject);
    }

}
