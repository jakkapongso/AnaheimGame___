using UnityEngine;
using System.Collections;

public class SpawnerMedium : MonoBehaviour
{
    [Header("Settings")]
    public GameObject enemyPrefab;    // ลาก Prefab ศัตรูมาใส่
    public Transform spawnPoint;      // ลากจุดเกิดมาใส่
    public float timeBetweenEnemies = 1.0f; // เวลาห่างระหว่างศัตรูแต่ละตัว

    [Header("Difficulty (Multiplier)")]
    public int initialEnemyCount = 2;  // จำนวนศัตรูเริ่มต้น
    public float multiplier = 0.2f;    // ตัวคูณความยาก (คูณ 1.5 ทุก Wave)

    private int currentWave = 0;
    private int enemiesToSpawn;

    void Start()
    {
        enemiesToSpawn = initialEnemyCount;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true) // วนลูปเกิด Wave ไปเรื่อยๆ
        {
            currentWave++;
            Debug.Log("เริ่ม Wave: " + currentWave + " จำนวนศัตรู: " + enemiesToSpawn);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(timeBetweenEnemies);
            }

            // คำนวณความยากแบบคูณสำหรับ Wave ถัดไป
            enemiesToSpawn = Mathf.RoundToInt(enemiesToSpawn * multiplier);

            // รอเวลาสักพักก่อนเริ่ม Wave ใหม่
            yield return new WaitForSeconds(2.0f);
        }
    }
}