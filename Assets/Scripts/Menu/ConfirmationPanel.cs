using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPanel : MonoBehaviour
{
    public delegate void ConfirmationAction();
    public event ConfirmationAction pressedYes;
    public event ConfirmationAction pressedNo;
    public static ConfirmationPanel singleton { get; private set; }

    private string _text;
    public string text { get => _text; set { _text = value; currentText.text = _text; } }

    private Animator anim;
    [SerializeField] private Text currentText;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        anim.SetBool("Open", true);
    }
    public void Close()
    {
        anim.SetBool("Open", false);
    }



    public void PressedYes()
    {
        pressedYes.Invoke();
        pressedYes = null;
    }

    public void PressedNo()
    {
        pressedNo.Invoke();
        pressedNo = null;
    }


}
