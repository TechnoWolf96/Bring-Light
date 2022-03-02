using UnityEngine;

public class Fader : MonoBehaviour
{
    public static Fader singleton { get; private set; } 

    public delegate void OnFadeAction(int number);
    public OnFadeAction onFadeAction;
    [HideInInspector] public int runSceneNumber;

    private Animator anim;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Fade()
    {
        anim.SetTrigger("Action");
    }

    public void FadeCompleted()
    {
        onFadeAction.Invoke(runSceneNumber);
        onFadeAction = null;
    }

    

}
