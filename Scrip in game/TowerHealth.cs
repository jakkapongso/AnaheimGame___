using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TowerHealth : MonoBehaviour
{
    [Header("Tower Status")]
    public float maxHp = 500f;
    private float currentHp;
    public string towerSide;

    [Header("UI Elements")]
    public Slider hpSlider;
    public GameObject gameOverPanel;
    public Image resultDisplayImage;
    public Sprite victorySprite;
    public Sprite loseSprite;

    [Header("Audio")]
    public AudioSource audioSource;  // ลำโพงสำหรับเอฟเฟกต์ชนะ/แพ้
    public AudioSource backgroundMusic; // <--- เพิ่มช่องนี้เพื่อลากลำโพงเพลง BGM มาใส่
    public AudioClip winSound;
    public AudioClip loseSound;

    private bool isGameOver = false;

    void Start()
    {
        currentHp = maxHp;
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHp;
            hpSlider.value = currentHp;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isGameOver) return;
        currentHp -= damage;
        if (hpSlider != null) hpSlider.value = currentHp;
        if (currentHp <= 0) ShowResult();
    }

    void ShowResult()
    {
        isGameOver = true;

        // --- เพิ่มคำสั่งหยุดเพลงหลังฉาก ---
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop(); // สั่งให้เพลง BGM หยุดทันที
        }

        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (towerSide == "HeroSide")
        {
            if (resultDisplayImage != null && loseSprite != null)
                resultDisplayImage.sprite = loseSprite;
            if (audioSource != null && loseSound != null)
                audioSource.PlayOneShot(loseSound);
        }
        else
        {
            if (resultDisplayImage != null && victorySprite != null)
                resultDisplayImage.sprite = victorySprite;
            if (audioSource != null && winSound != null)
                audioSource.PlayOneShot(winSound);
        }
    }

    public void ClickRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ClickMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}