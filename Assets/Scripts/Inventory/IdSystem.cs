using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdSystem : MonoBehaviour
{
    private static IdSystem _singleton;
    public static IdSystem singleton { get => _singleton; }


    [SerializeField] private List<GameObject> _icons;
    public List<GameObject> icons { get => _icons;}


    private void Awake()
    {
        _singleton = this;
    }
}
