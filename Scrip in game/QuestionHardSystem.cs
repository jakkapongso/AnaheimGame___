using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionHardSystem : MonoBehaviour
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
        // สุ่มเลขฐาน 2-10 และเลขชี้กำลัง 2-3
        int baseNum = Random.Range(2, 11);
        int exponent = Random.Range(2, 4);
        correctAnswer = (int)Mathf.Pow(baseNum, exponent);

        // ใช้ <sup> เพื่อแสดงเลขยกกำลังบน UI
        questionText.text = baseNum + "<sup>" + exponent + "</sup> = ?";

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
                displayValue = correctAnswer + Random.Range(-20, 21);
                if (displayValue <= 0 || displayValue == correctAnswer) displayValue = correctAnswer + Random.Range(1, 10);
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
            // ตอบถูก: เสกตัวละครและสุ่มข้อใหม่
            Debug.Log("Correct!");
            SpawnHero();
            GenerateQuestion();
        }
        else
        {
            // ตอบผิด: เปลี่ยนข้อทันที (ข้ามไปข้อถัดไป)
            Debug.Log("Wrong! Next question...");
            GenerateQuestion();
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