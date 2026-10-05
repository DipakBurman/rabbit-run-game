using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Hook these up to the OnClick events of the menu / HUD buttons.
public class UIButtons : MonoBehaviour
{
    private const string SoundPrefKey = "SoundOn";

    [SerializeField] private string gameScene = "Game";
    [SerializeField] private string menuScene = "Menu";
    [SerializeField] private TMP_Text soundLabel;

    private void Awake()
    {
        ApplySound(IsSoundOn());
    }

    public void OnStartButtonClicked()
    {
        GameProgress.Level = 1;
        SceneManager.LoadScene(gameScene);
    }

    public void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void OnNextLevelButtonClicked()
    {
        GameProgress.Level++;
        SceneManager.LoadScene(gameScene);
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void OnSoundButtonClicked()
    {
        bool soundOn = !IsSoundOn();
        PlayerPrefs.SetInt(SoundPrefKey, soundOn ? 1 : 0);
        PlayerPrefs.Save();
        ApplySound(soundOn);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private static bool IsSoundOn()
    {
        return PlayerPrefs.GetInt(SoundPrefKey, 1) == 1;
    }

    private void ApplySound(bool soundOn)
    {
        AudioListener.volume = soundOn ? 1f : 0f;
        if (soundLabel != null) soundLabel.text = soundOn ? "SOUND ON" : "SOUND OFF";
    }
}
