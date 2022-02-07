using UnityEngine;

public class DescriptionPanel : MonoBehaviour
{
    private static GameObject _singleton;
    public static GameObject singleton { get => _singleton; }


    private void Awake()
    {
        _singleton = gameObject;
    }

}
