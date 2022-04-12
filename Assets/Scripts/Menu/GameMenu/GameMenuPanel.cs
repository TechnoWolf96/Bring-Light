using UnityEngine;

public class GameMenuPanel : MonoBehaviour
{
    public static GameMenuPanel singleton { get; protected set; }
    [SerializeField] private GameObject gameMenuPanel;
    public bool pause { get; protected set; }

    private void Awake()
    {
        singleton = this;
        pause = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        gameMenuPanel.SetActive(true);
        pause = true;
        Player.singleton.pause = true;
        Control.singleton.playerControlActive = false;
        Control.singleton.playerInterfaceActive = false;
    }

    public void Continue()
    {
        pause = false;
        Time.timeScale = 1f;
        gameMenuPanel.SetActive(false);
        Player.singleton.pause = false;
        Control.singleton.playerInterfaceActive = true;
        if (!InventoryPanel.singleton.anim.GetBool("Open")) 
            Control.singleton.playerControlActive = true;
    }


}
