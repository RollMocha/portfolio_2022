using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� ������ �� ������ ����ü ��ũ��Ʈ
public class Bullet : MonoBehaviour
{
    protected Enemy_1 attackTarget; // �����ؾ��� �� ����
    public float destroyTime = 10f; // ������ �������� ��� ���
    public float speed; // ���󰡴� źȯ�� �ӵ�
    public int damage; // źȯ�� ������

    public bool isSplash = false; // ���÷��� �������� Ȯ��
    public float hitRange; // ���÷��� ����

    public bool isSlow = false; // ���ο� �������� Ȯ��
    public float slowPercent; // ���ο� ��ġ
    public int slowTime; // ���ο� �ð�

    public bool isBondage = false; // �ӹ� �������� Ȯ��
    public float bondageTime; // �ӹ� �ð�

    public bool isKnockBack = false; // �˹� �������� Ȯ��
    public int knockBackPower; // �˹� ��ġ

    public ParticleSystem hitEffact; // �ǰ� �� ȿ��

    void Start()
    {
        // ������ ���� ���� ��츦 ���� destroyTime�ð��� ������ ����
        Destroy(this.gameObject, destroyTime);
    }

    // źȯ�� �ӵ��� �����ϰ� ���߱� ���� ���
    private void FixedUpdate()
    {
        if (attackTarget == null)
        {
            // ���� ���� ��� ����
            Destroy(this.gameObject, 0.1f);
        }
        else
        {
            // ���� ���� ��� �Լ� ����
            MoveToTarget();
        }
    }

    // ���� ���� �� źȯ ������ �����ϴ� �Լ�
    // Slime ��ũ��Ʈ���� ȣ��
    public void SetTarget(Enemy_1 target, float bulletSpeed, int bulletDamage)
    {
        attackTarget = target;
        speed = bulletSpeed;
        damage = bulletDamage;
    }

    // ���� ���� ���󰡴� �Լ�
    void MoveToTarget()
    {
        if (attackTarget == null)
        {
            // ���� ���� ��� �ٷ� ����
            return;
        }

        if (speed <= 0)
        {
            // �ӵ��� 0���� ���� ��� ����
            return;
        }

        // �� ��ġ�� ����
        transform.position =
            Vector3.MoveTowards(this.transform.position, attackTarget.transform.position, speed);

    }

    // ���� �ε��� �� ������ �����ϴ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Monster")) // ���� �ױ׸� ������ �ʾ��� ���
        {
            return;
        }

        // �΋H�� �� ���� ��������
        Enemy_1 enemy = other.GetComponent<Enemy_1>();

        if (enemy != attackTarget) // ���� ���� Ÿ������ Ȯ��
        {
            return;
        }

        // ���÷��� ������ ��� ����
        if (isSplash)
        {
            // ������ ���濡�� ���� ��� ������ ���� ����� �ֺ��� ��� ������ ����
            // ���� bullet�� ��ġ(transform.position), ���÷��� ����(hitRange) �ʿ�
            Collider[] hitCol = Physics.OverlapSphere(transform.position, hitRange);

            // ������ ���� �΋H�� ����(Collioder �迭)���� ������ �ϳ��� ����
            foreach (Collider hit in hitCol)
            {
                if (hit.tag != "Monster") // ������ ���� �ƴҰ�� �ٷ� ����
                {
                    continue;
                }

                // �� ���� ��������
                Enemy_1 enemy_ = hit.GetComponent<Enemy_1>();

                if (isSlow) // ���ο� ����
                {
                    enemy_.SlowDebuff(slowPercent, slowTime);
                }

                if (isBondage) // �ӹ� ����
                {
                    enemy_.BondageDebuff(bondageTime);
                }

                if (isKnockBack) // �˹� ����
                {
                    enemy.KnockBackDebuff(transform.position, knockBackPower);
                }

                enemy_.Damage(damage); // ������ ������ ����
            }

        }
        else
        {
            if (isSlow) // ���ο� ����
            {
                enemy.SlowDebuff(slowPercent, slowTime);
            }

            if (isBondage) // �ӹ� ����
            {
                enemy.BondageDebuff(bondageTime);
            }

            if (isKnockBack) // �˹� ����
            {
                enemy.KnockBackDebuff(transform.position, knockBackPower);
            }

            enemy.Damage(damage); // ������ ������ ����
        }

        if (hitEffact != null)
        {
            ParticleSystem particle = Instantiate(hitEffact, transform.position, Quaternion.identity);
            particle.Play();
        }

        // ������ �΋H�� bullet ����
        Destroy(this.gameObject, 0.1f);
    }
}