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

    private void Start()
    {
        light = GetComponent<Light2D>();
        if (startWithRandomIntensity) light.color.a = Random.Range(minIntensity, 1f);
        else light.color.a = startIntensity;
    }
    private void FixedUpdate()
    {
        if (increase)
        {
            light.color.a += Time.deltaTime * speedChanging;
            if (light.color.a >= 1) increase = false;
        }
        else
        {
            light.color.a -= Time.deltaTime * speedChanging;
            if (light.color.a < minIntensity) increase = true;
        }
    }

}
