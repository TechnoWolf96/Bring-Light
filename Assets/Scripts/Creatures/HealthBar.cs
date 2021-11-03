using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform valueSlider;
    public Animator animSlider;
    private Creature creature;


    private void Start()
    {
        creature = GetComponent<Creature>();
    }


    public void ShowBar()
    {
        Vector3 newScale = valueSlider.localScale;
        newScale.x = (float)creature.health / creature.maxHealth;
        valueSlider.localScale = newScale;
        animSlider.SetTrigger("ShowBar");
    }
}
