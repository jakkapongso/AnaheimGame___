using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionMediumSystem : MonoBehaviour
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
        int val1 = Random.Range(2, 11);
        int val2 = Random.Range(2, 11);
        correctAnswer = val1 * val2;

        questionText.text = val1 + " x " + val2 + " = ?";

        int correctButtonIndex = Random.Range(0, answerButtons.Length);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int displayValue = (i == correctButtonIndex) ? correctAnswer : correctAnswer + Random.Range(-10, 11);
            if (displayValue == correctAnswer && i != correctButtonIndex) displayValue++;

            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = displayValue.ToString();

            int index = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index, correctIndex: correctButtonIndex));
        }
    }

    public void ClickReset()
    {
        Debug.Log("Resetting Game...");
        GenerateQuestion();
    }

    void CheckAnswer(int selectedIndex, int correctIndex)
    {
        if (selectedIndex == correctIndex)
        {
            // ตอบถูก: เสกฮีโร่และเปลี่ยนข้อ
            Instantiate(heroPrefab, spawnPoint.position, Quaternion.identity);
            GenerateQuestion();
        }
        else
        {
            // --- ส่วนที่แก้ไข: ตอบผิดให้เปลี่ยนข้อทันทีป้องกันการกดเดา ---
            Debug.Log("Wrong Answer! Changing question...");
            GenerateQuestion();
            // ---------------------------------------------------
        }
    }
}