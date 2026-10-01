using UnityEngine;
using UnityEngine.SceneManagement;

// Hook these up to the OnClick events of the menu / HUD buttons.
public class UIButtons : MonoBehaviour
{
    [SerializeField] private string gameScene = "Game";
    [SerializeField] private string menuScene = "Menu";

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
