using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weight : MonoBehaviour
{
    private static Weight _singleton;
    public static Weight singleton { get => _singleton; }

    [SerializeField] private float _maxWeight;
    public float maxWeight
    {
        get => _maxWeight;
        set
        {
            _maxWeight = value;
            UpdateWeight();
        }
    }
    private float _totalWeight;
    public float totalWeight
    {
        get => _totalWeight;
        set
        {
            _totalWeight = value;
            UpdateWeight();
        }
    }

    private Text weightText;
    private Slider weightSlider;

    private void Awake()
    {
        _singleton = this;
    }

    private void Start()
    {
        weightText = GameObject.Find("Canvas/Inventory/BackpackPanel/Weight/Text").GetComponent<Text>();
        weightSlider = GameObject.Find("Canvas/Inventory/BackpackPanel/Weight").GetComponent<Slider>();
        totalWeight = InventoryInfoSlot.singleton.GetTotalWeight();
    }



    private void UpdateWeight()
    {
        weightText.text = totalWeight.ToString("0.0") + "/" + maxWeight.ToString();
        //weightText.text = totalWeight.ToString() + "/" + maxWeight.ToString();
        weightSlider.value = totalWeight / maxWeight;
    }



}
