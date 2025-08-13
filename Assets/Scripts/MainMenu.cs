using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject settingsPanel;
    public GameObject aboutPanel;

    void Awake()
    {
        Time.timeScale = 1f;
    }

    // Buttons
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");  
    }

    public void OpenSettings()
    {
        // SHow only settings (hide about or other windows)
        // TODO: hide other windows should be automaticly when something new is opened
        if (settingsPanel) settingsPanel.SetActive(true);
        if (aboutPanel)    aboutPanel.SetActive(false);
    }

    public void OpenAbout()
    {
        // SHow only about (hide settings or other windows)
        // TODO: hide other windows should be automaticly when something new is opened
        if (settingsPanel) settingsPanel.SetActive(false);
        if (aboutPanel)    aboutPanel.SetActive(true);
    }

    public void BackToMain()
    {
        // Hide everything
        if (settingsPanel) settingsPanel.SetActive(false);
        if (aboutPanel)    aboutPanel.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Debug.Log("Quit is not supported on WebGL.");
#else
        Application.Quit();
#endif
    }
}
