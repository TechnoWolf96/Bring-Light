using UnityEngine;

public class GameMenuPanel : MonoBehaviour
{
    public static GameMenuPanel singleton { get; protected set; }
    [SerializeField] private GameObject gameMenuPanel;
    private float lastTimeScale;
    private bool lastPlayerPause;
    public bool pause { get; protected set; }

    private void Awake()
    {
        singleton = this;
        pause = false;
    }

    public void Pause()
    {
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        gameMenuPanel.SetActive(true);
        pause = true;
        lastPlayerPause = Player.singleton.pause;
        Player.singleton.pause = true;
        Control.singleton.playerControlActive = false;
        Control.singleton.playerInterfaceActive = false;
    }

    public void Continue()
    {
        pause = false;
        Time.timeScale = lastTimeScale;
        gameMenuPanel.SetActive(false);
        Player.singleton.pause = lastPlayerPause;
        Control.singleton.playerInterfaceActive = true;
        if (!InventoryPanel.singleton.anim.GetBool("Open")) 
            Control.singleton.playerControlActive = true;
    }


}
