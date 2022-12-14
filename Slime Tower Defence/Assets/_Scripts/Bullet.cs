using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 슬라임이 공격할 때 나가는 투사체 스크립트
public class Bullet : MonoBehaviour
{
    protected Enemy_1 attackTarget; // 공격해야할 적 정보
    public float destroyTime = 10f; // 공격이 맞지않을 경우 대비
    public float speed; // 날라가는 탄환의 속도
    public int damage; // 탄환의 데미지

    public bool isSplash = false; // 스플레시 공격인지 확인
    public float hitRange; // 스플레시 범위

    public bool isSlow = false; // 슬로우 공격인지 확인
    public float slowPercent; // 슬로우 수치
    public int slowTime; // 슬로우 시간

    public bool isBondage = false; // 속박 공격인지 확인
    public float bondageTime; // 속박 시간

    public bool isKnockBack = false; // 넉백 공격인지 확인
    public int knockBackPower; // 넉백 수치

    public ParticleSystem hitEffact; // 피격 시 효과

    void Start()
    {
        // 공격이 맞지 않을 경우를 위해 destroyTime시간이 지나면 제거
        Destroy(this.gameObject, destroyTime);
    }

    // 탄환의 속도를 일정하게 맞추기 위해 사용
    private void FixedUpdate()
    {
        if (attackTarget == null)
        {
            // 적이 없을 경우 삭제
            Destroy(this.gameObject, 0.1f);
        }
        else
        {
            // 적이 있을 경우 함수 실행
            MoveToTarget();
        }
    }

    // 적의 정보 및 탄환 정보를 세팅하는 함수
    // Slime 스크립트에서 호출
    public void SetTarget(Enemy_1 target, float bulletSpeed, int bulletDamage)
    {
        attackTarget = target;
        speed = bulletSpeed;
        damage = bulletDamage;
    }

    // 적을 향해 날라가는 함수
    void MoveToTarget()
    {
        if (attackTarget == null)
        {
            // 적이 없을 경우 바로 리턴
            return;
        }

        if (speed <= 0)
        {
            // 속도가 0보다 작을 경우 리턴
            return;
        }

        // 적 위치로 날라감
        transform.position =
            Vector3.MoveTowards(this.transform.position, attackTarget.transform.position, speed);

    }

    // 적과 부딪힐 때 공격을 실행하는 함수
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Monster")) // 몬스터 테그를 가지지 않았을 경우
        {
            return;
        }

        // 부딫힌 적 정보 가져오기
        Enemy_1 enemy = other.GetComponent<Enemy_1>();

        if (enemy != attackTarget) // 맞은 적이 타겟인지 확인
        {
            return;
        }

        // 스플레시 공격인 경우 실행
        if (isSplash)
        {
            // 공격이 상대방에게 닿을 경우 가상의 원을 만들어 주변의 모든 적들을 감지
            // 현재 bullet의 위치(transform.position), 스플레시 범위(hitRange) 필요
            Collider[] hitCol = Physics.OverlapSphere(transform.position, hitRange);

            // 가상의 원에 부딫힌 정보(Collioder 배열)들을 가져와 하나씩 실행
            foreach (Collider hit in hitCol)
            {
                if (hit.tag != "Monster") // 맞은게 적이 아닐경우 바로 리턴
                {
                    continue;
                }

                // 적 정보 가져오기
                Enemy_1 enemy_ = hit.GetComponent<Enemy_1>();

                if (isSlow) // 슬로우 실행
                {
                    enemy_.SlowDebuff(slowPercent, slowTime);
                }

                if (isBondage) // 속박 실행
                {
                    enemy_.BondageDebuff(bondageTime);
                }

                if (isKnockBack) // 넉백 실행
                {
                    enemy.KnockBackDebuff(transform.position, knockBackPower);
                }

                enemy_.Damage(damage); // 적에게 데미지 전달
            }

        }
        else
        {
            if (isSlow) // 슬로우 실행
            {
                enemy.SlowDebuff(slowPercent, slowTime);
            }

            if (isBondage) // 속박 실행
            {
                enemy.BondageDebuff(bondageTime);
            }

            if (isKnockBack) // 넉백 실행
            {
                enemy.KnockBackDebuff(transform.position, knockBackPower);
            }

            enemy.Damage(damage); // 적에게 데미지 전달
        }

        if (hitEffact != null)
        {
            ParticleSystem particle = Instantiate(hitEffact, transform.position, Quaternion.identity);
            particle.Play();
        }

        // 적에게 부딫힌 bullet 제거
        Destroy(this.gameObject, 0.1f);
    }
}