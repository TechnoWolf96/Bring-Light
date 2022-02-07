using FunkyCode;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    //public float maxIntensity;
    public float minIntensity;
    public float startIntensity = 0;
    public float speedChanging;
    public bool startWithRandomIntensity;

    private Light2D light;
    private bool increase = true;
    private float maxIntensity;

    private void Start()
    {
        light = GetComponent<Light2D>();
        maxIntensity = light.color.a;
        if (startWithRandomIntensity) light.color.a = Random.Range(minIntensity, maxIntensity);
        else light.color.a = startIntensity;
    }
    private void FixedUpdate()
    {
        if (increase)
        {
            light.color.a += Time.deltaTime * speedChanging;
            if (light.color.a >= maxIntensity) increase = false;
        }
        else
        {
            light.color.a -= Time.deltaTime * speedChanging;
            if (light.color.a < minIntensity) increase = true;
        }
    }

}
