using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public static InventoryPanel singleton { get; private set; }
    public Animator anim { get; private set; }

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void OpenOrClose()
    {
        if (anim.GetBool("Open")) { Close(); Time.timeScale = 1f; Player.singleton.pause = false; }
        else { Open(); Time.timeScale = 0f; Player.singleton.pause = true; }
    }

    private void Open()
    {
        Control.singleton.playerControlActive = false;
        anim.SetBool("Open", true);
    }

    private void Close()
    {
        Control.singleton.playerControlActive = true;
        anim.SetBool("Open", false);
    }



}
