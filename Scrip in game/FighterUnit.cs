using UnityEngine;
using UnityEngine.UI;

public class FighterUnit : MonoBehaviour
{
    [Header("Status")]
    public float hp = 100f;
    private float maxHp;
    public float moveSpeed = 2f;
    public float attackRange = 1.2f;
    public float attackDamage = 20f;
    public float attackRate = 1f;
    private float nextAttackTime = 0f;

    [Header("UI Section")]
    public Slider hpSlider;

    [Header("Audio Section")] // --- ส่วนที่เพิ่มใหม่ ---
    public AudioSource audioSource;   // ลำโพง
    public AudioClip attackSound;    // เสียงตอนฟันดาบ
    public AudioClip dieSound;       // เสียงตอนตัวละครตาย (ถ้ามี)

    [Header("Targets")]
    public string enemyTag;
    public string enemyTowerTag;

    private GameObject currentTarget;
    private Animator anim;
    private bool isDead = false;

    void Start() {
        anim = GetComponent<Animator>();
        // ดึง AudioSource อัตโนมัติถ้าลืมลากใส่
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        maxHp = hp;
        if (hpSlider != null) {
            hpSlider.maxValue = maxHp;
            hpSlider.value = hp;
        }
    }

    void Update() {
        if (isDead) return;
        FindTarget();
        MoveAndCombat();
    }

    void FindTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null) {
            currentTarget = nearestEnemy;
        } else {
            currentTarget = GameObject.FindWithTag(enemyTowerTag);
        }
    }

    void MoveAndCombat() {
        if (currentTarget == null) {
            anim.SetBool("isWalking", false);
            return;
        }

        float distance = Vector3.Distance(transform.position, currentTarget.transform.position);

        if (distance > attackRange) {
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            anim.SetBool("isWalking", true);

            if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else {
            anim.SetBool("isWalking", false);
            if (Time.time >= nextAttackTime) {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack() {
        if (currentTarget == null) return;

        anim.SetTrigger("Attack");

        // --- จุดที่ 1: เล่นเสียงตอนฟันดาบ ---
        if (audioSource != null && attackSound != null) {
            audioSource.PlayOneShot(attackSound);
        }

        FighterUnit targetUnit = currentTarget.GetComponent<FighterUnit>();
        if (targetUnit != null) {
            targetUnit.TakeDamage(attackDamage);
        }
        else {
            currentTarget.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void TakeDamage(float damage) {
        if (isDead) return;
        hp -= damage;
        if (hpSlider != null) hpSlider.value = hp;
        if (hp <= 0) Die();
    }

    void Die() {
        if (isDead) return;
        isDead = true;

        // --- จุดที่ 2: เล่นเสียงตอนตาย ---
        if (audioSource != null && dieSound != null) {
            audioSource.PlayOneShot(dieSound);
        }

        if (hpSlider != null) hpSlider.gameObject.SetActive(false);
        if (GetComponent<Collider2D>() != null) GetComponent<Collider2D>().enabled = false;

        anim.SetTrigger("Die");
        Destroy(gameObject, 0.8f); // เพิ่มเวลาทำลายเล็กน้อยให้อติเมชั่น/เสียงเล่นจบ
    }
}