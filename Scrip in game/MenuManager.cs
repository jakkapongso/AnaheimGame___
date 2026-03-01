using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainPanel;       // ลากกลุ่มหน้าแรกมาใส่ตรงนี้
    public GameObject difficultyPanel; // ลากกลุ่มหน้าเลือกความยากมาใส่ตรงนี้

    // ฟังก์ชันสำหรับปุ่ม "เริ่มเกม"
    public void OpenDifficulty()
    {
        mainPanel.SetActive(false);     // ปิดหน้าแรก
        difficultyPanel.SetActive(true); // เปิดหน้าเลือกความยาก
    }

    // ฟังก์ชันสำหรับปุ่ม "ย้อนกลับ"
    public void BackToMain()
    {
        difficultyPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // --- ฟังก์ชันที่เพิ่มใหม่สำหรับโหลดฉากคู่มือ ---
    public void GoToManual()
    {
        SceneManager.LoadScene("Manual"); // โหลด Scene ที่ชื่อ Manual ทันที
    }

    // ฟังก์ชันโหลดเข้าเกมตามระดับความยาก
    public void PlayEasy() { SceneManager.LoadScene("SampleScene"); }
    public void PlayMedium() { SceneManager.LoadScene("GameMedium"); }
    public void PlayHard() { SceneManager.LoadScene("GameHard"); }
}