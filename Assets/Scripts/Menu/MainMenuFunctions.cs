using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    public void NewGame_ButtonPressed()
    {
        var confirm = ConfirmationPanel.singleton;
        confirm.Open();
        confirm.text = "Начать новую игру? Предыдущий прогресс будет удален.";
        confirm.pressedYes += BeginNewGame;
        confirm.pressedNo += CancelConfirm;
    }

    public void Exit_ButtonPressed()
    {
        var confirm = ConfirmationPanel.singleton;
        confirm.Open();
        confirm.text = "Выйти из игры?";
        confirm.pressedYes += ExitGame;
        confirm.pressedNo += CancelConfirm;
        
    }

    public void Continue_ButtonPressed()
    {
        Fader fader = Fader.singleton;
        fader.runSceneNumber = 2;
        fader.onFadeAction += SceneManager.LoadScene;
        fader.Fade();
        ConfirmationPanel.singleton.Close();
        MenuPanel.singleton.Close();
    }

    public void OpenSettings_ButtonPressed()
    {
        SettingsPanel.singleton.Open();
        MenuPanel.singleton.Close();
        ConfirmationPanel.singleton.Close();
    }

    public void OpenControlSettings_ButtonPressed()
    {
        SettingsPanel.singleton.Close();
        ControlPanel.singleton.Open();
    }



    private void ExitGame()
    {
        Fader fader = Fader.singleton;
        fader.onFadeAction += Application.Quit;
        fader.Fade();
        ConfirmationPanel.singleton.Close();
        MenuPanel.singleton.Close();
    }


    private void BeginNewGame()
    {
        Fader fader = Fader.singleton;
        fader.runSceneNumber = 1;
        fader.onFadeAction += SceneManager.LoadScene;
        fader.Fade();
        ConfirmationPanel.singleton.Close();
        MenuPanel.singleton.Close();
    }

    private void CancelConfirm()
    {
        ConfirmationPanel.singleton.Close();
    }



}
