using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

    [Header("Game Elements")]
    public GameObject heroPrefab;
    public Transform spawnPoint;

    private int correctAnswer;

    void Start()
    {
        GenerateQuestion();
    }

    public void GenerateQuestion()
    {
        int val1 = Random.Range(1, 11); // ปรับให้ถึง 10
        int val2 = Random.Range(1, 11);
        correctAnswer = val1 + val2;

        questionText.text = val1 + " + " + val2 + " = ?";

        int correctButtonIndex = Random.Range(0, answerButtons.Length);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int displayValue;
            if (i == correctButtonIndex)
            {
                displayValue = correctAnswer;
            }
            else
            {
                displayValue = correctAnswer + Random.Range(-5, 6);
                if (displayValue == correctAnswer) displayValue++;
            }

            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = displayValue.ToString();

            int index = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index, correctButtonIndex));
        }
    }

    void CheckAnswer(int selectedIndex, int correctIndex)
    {
        if (selectedIndex == correctIndex)
        {
            Debug.Log("Correct!");
            SpawnHero();
            GenerateQuestion();
        }
        else
        {
            // --- ส่วนที่แก้ไข: ตอบผิดให้เปลี่ยนข้อทันที ---
            Debug.Log("Wrong Answer! Changing question...");
            GenerateQuestion();
            // ---------------------------------------
        }
    }

    void SpawnHero()
    {
        if (heroPrefab != null && spawnPoint != null)
        {
            Instantiate(heroPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}