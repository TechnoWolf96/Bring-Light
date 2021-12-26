using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float maxIntensity;
    public float minIntensity;
    public float speedChanging;

    private Light light;
    private bool increase = true;

    private void Start()
    {
        light = GetComponent<Light>();
        light.intensity = minIntensity;
    }
    private void FixedUpdate()
    {
        if (increase)
        {
            light.intensity += Time.deltaTime * speedChanging;
            if (light.intensity > maxIntensity) increase = false;
        }
        else
        {
            light.intensity -= Time.deltaTime * speedChanging;
            if (light.intensity < minIntensity) increase = true;
        }
    }

}
