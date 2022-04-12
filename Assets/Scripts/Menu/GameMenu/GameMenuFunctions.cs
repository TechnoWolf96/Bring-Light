using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuFunctions : MonoBehaviour
{
    public void ExitGame()
    {
        Fader fader = Fader.singleton;
        fader.onFadeAction += Application.Quit;
        fader.Fade();
    }

    public void ToMainMenu()
    {
        Fader fader = Fader.singleton;
        fader.runSceneNumber = 0;
        fader.onFadeAction += SceneManager.LoadScene;
        fader.onFadeAction += TimeContinue;
        fader.Fade();
    }

    public void TimeContinue(int noValue = 0) => Time.timeScale = 1f;



}
