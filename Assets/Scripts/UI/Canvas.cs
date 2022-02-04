using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{

    private static GameObject _singleton;
    public static GameObject singleton { get => _singleton; }

    private void Awake()
    {
        _singleton = gameObject;
    }
}
