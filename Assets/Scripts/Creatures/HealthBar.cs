using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform valueSlider;
    private Animator animSlider;
    private Creature creature;


    private void Start()
    {
        valueSlider = transform.Find("Value");
        animSlider = GetComponent<Animator>();
        creature = GetComponentInParent<Creature>();
        creature.onHealthChanged += ShowBar;
    }


    public void ShowBar()
    {
        Vector3 newScale = valueSlider.localScale;
        newScale.x = (float)creature.health / creature.maxHealth;
        valueSlider.localScale = newScale;
        animSlider.SetTrigger("ShowBar");
    }
}
