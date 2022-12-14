using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeState
{
    DEFAULT,
    ICE,
    FIRE,
    THUNDER,
    VINE,
    WATER,
    WIND
};

public class Slime : MonoBehaviour
{
    bool isAttack = true; // ������ �������� Ȯ��

    public List<GameObject> enemyList; // �� ����Ʈ
    public GameObject bulletPrefab; // ���� ������

    Enemy_1 targetEnemy = null; // ��ǥ �� ����
    private AudioSource audioSource;
    public AudioClip audio;

    public float attackSpeed; // ���� �ӵ�
    public float bulletSpeed; // ����ü �ӵ�
    public float attackRange; // ���� ���� 
    public int attackDamage; // ���ݷ�

    float timer = 0; // ���� �ӵ� ������ ���� ���

    public SlimeState state; // �������� ���� Ȯ��

    Animator slimeAnimator; // ������ �ִϸ��̼� ����

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audio;
        audioSource.Play();

        // �ִϸ��̼��� �ִ��� Ȯ��
        slimeAnimator = GetComponentInChildren<Animator>();
        if (slimeAnimator == null)
        {
            Debug.Log("ani war"); // �ִϸ��̼� ����
        }
        else
        {
            // �ִϸ��̼��� �ִ� ���
            // ���� �ӵ��� ���� �ִϸ��̼� �ӵ� ����
            slimeAnimator.speed = slimeAnimator.speed * 3 / attackSpeed;
        }
    }

    void Update()
    {
        timer += Time.deltaTime; // ���ݼӵ��� ���� ���

        // ���� �ʿ� �ִ��� Ȯ��
        if (WaveSpawner.waveSpawner.EnemyList_1.Count <= 0)
        {
            return;
        }

        // ���� ���� ���� ������ �߰�
        if (targetEnemy == null)
        {
            targetEnemy = FindEnemyClosestToTower();
            return;
        }
        else
        {
            // ���� ��Ÿ� �ȿ� �ִ��� Ȯ��
            float dir = Vector3.Distance(transform.position, targetEnemy.transform.position);

            if (dir > attackRange)
            {
                targetEnemy = null;
                return;
            }

            // �� �ٶ󺸱�
            RotateToTarget(targetEnemy);

            // ���� �ð� üũ
            if (timer > attackSpeed)
            {
                Attack(targetEnemy);
                timer = 0;
            }
        }
    }

    // ������ ���� �Լ�
    void Attack(Enemy_1 target)
    {
        if (isAttack) // ������ �������� Ȯ��
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x,
                transform.position.y, transform.position.z), Quaternion.identity); // ���� ������ ����

            bullet.GetComponent<Bullet>().SetTarget(target, bulletSpeed,
                attackDamage); // ���� �����տ� �� ���� ����

            // �ִϸ��̼��� ������ ����
            if (slimeAnimator != null)
            {
                slimeAnimator.SetTrigger("attack");
            }
        }

    }

    // ���� �ٶ󺸴� �Լ�
    void RotateToTarget(Enemy_1 target)
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y,
            target.transform.position.z);

        transform.LookAt(targetPosition);
    }

    // ���� ����� �� ����
    public Enemy_1 FindEnemyClosestToTower()
    {
        Enemy_1 target_ = null; // ��ȯ�� �� ����
        float minDir = -1; // ���� ����� �Ÿ� �����

        // �� ����Ʈ���� ���� ����� �� ã��
        foreach (Enemy_1 enemy in WaveSpawner.waveSpawner.EnemyList_1)
        {
            // ������ �Ÿ� ���
            float dir = Vector3.Distance(transform.position, enemy.transform.position);

            // ���� ��Ÿ� �ȿ� �ִ��� Ȯ��
            if (dir > attackRange)
            {
                continue;
            }

            // ���� ���� ������ �ִ��� Ȯ��
            if (dir > minDir)
            {
                minDir = dir;
                target_ = enemy;
            }
        }

        return target_; // ���������� �� ��ȯ
    }
}
