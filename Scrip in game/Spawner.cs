using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    // ตรงนี้ต้องชื่อ EnemySpawner นะครับ ห้ามเป็น FighterUnit

    public GameObject enemyPrefab; // ลากตัวศัตรูมาใส่ตรงนี้
    public float spawnInterval = 5f; // เวลาเกิด

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // สร้างศัตรูออกมา
            if (enemyPrefab != null)
            {
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }

            // รอเวลา 5 วินาที
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}