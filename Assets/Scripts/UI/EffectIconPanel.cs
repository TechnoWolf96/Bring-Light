using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectIconPanel : MonoBehaviour
{
    private static EffectIconPanel _sigleton;
    public static EffectIconPanel sigleton { get => _sigleton; }

    private void Awake()
    {
        _sigleton = this;
    }

    public void AddEffect(GameObject newEffectObject, float duration)
    {
        Instantiate(newEffectObject, transform).GetComponent<EffectIcon>().duration = duration;
    }



}
