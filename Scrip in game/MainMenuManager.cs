using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainPanel;
    public GameObject difficultyPanel;

    public void OpenDifficulty()
    {
        mainPanel.SetActive(false);
        difficultyPanel.SetActive(true);
    }

    public void BackToMain()
    {
        difficultyPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void GoToManual()
    {
        SceneManager.LoadScene("Manual"); // โหลดไปฉากคู่มือ
    }

    public void PlayEasy() { SceneManager.LoadScene("SampleScene"); }
    public void PlayMedium() { SceneManager.LoadScene("GameMedium"); }
    public void PlayHard() { SceneManager.LoadScene("GameHard"); }
}