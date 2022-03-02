using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    public void NewGame_ButtonPressed()
    {
        var confirm = Confirmation.singleton;
        confirm.Open();
        confirm.text = "Начать новую игру? Предыдущий прогресс будет удален.";
        confirm.pressedYes += BeginNewGame;
        confirm.pressedNo += CancelConfirm;
    }

    public void Exit_ButtonPressed()
    {
        var confirm = Confirmation.singleton;
        confirm.Open();
        confirm.text = "Выйти из игры?";
        confirm.pressedYes += Quit;
        confirm.pressedNo += CancelConfirm;
    }

    public void Continue_ButtonPressed()
    {
        Fader fader = Fader.singleton;
        fader.runSceneNumber = 2;
        fader.onFadeAction += SceneManager.LoadScene;
        fader.Fade();
    }

    public void Settings_ButtonPressed()
    {
        SettingsPanel.singleton.Open();
    }


    private void Quit()
    {
        Confirmation.singleton.pressedYes -= Quit;
        Fader fader = Fader.singleton;
        fader.onFadeAction += Application.Quit;
        fader.Fade();
    }


    private void BeginNewGame()
    {
        Confirmation.singleton.pressedYes -= BeginNewGame;
        Fader fader = Fader.singleton;
        fader.runSceneNumber = 1;
        fader.onFadeAction += SceneManager.LoadScene;
        fader.Fade();
    }

    private void CancelConfirm()
    {
        Confirmation.singleton.pressedNo -= CancelConfirm;
        Confirmation.singleton.Close();
    }



}
