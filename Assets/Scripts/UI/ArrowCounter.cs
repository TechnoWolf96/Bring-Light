using UnityEngine;
using UnityEngine.UI;

public class ArrowCounter : MonoBehaviour
{
    private static ArrowCounter _singleton;
    public static ArrowCounter singleton { get => _singleton; }

    private Sprite _sprite;
    public Sprite sprite { get => _sprite; set { _sprite = value; image.sprite = _sprite; image.color = new Color(1,1,1,1); } }
    [SerializeField] private Image image;

    private int _count;
    public int count { get => _count; set { _count = value; counter.text = _count.ToString(); } }
    [SerializeField] private Text counter;

    public void RemoveArrows()
    {
        count = 0;
        image.color = new Color(1, 1, 1, 0);
    }

    private void Awake()
    {
        _singleton = this;
    }



}
