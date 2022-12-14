using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_1 : MonoBehaviour
{
    public int maxHp = 10;//몬스터 체력
    int currentHp; // 현재 체력
    public float defaultSpeed = 10f;//몬스터 기본 속도 : 2022.5.27 추가

    public float destroy_time = 0.1f;//최종도착과 디스폰 사이 시간
    public Transform[] fruits;//
    public int fruitspawnrandom = 20;

    Rigidbody E1_rigidbody; //Rigidbody를 저장하는 변수
    public int rotatespeed = 5; //회전속도
    public int fruitsindex = 3;
    private Transform target;//Transform
    private int wavepointIndex = 0;//OneWaypoints의 인덱스
    private int Waypoint;// 웨이브 경로 종류
    private int wayPointCount; //이동 경로 갯수
    private WaveSpawner waveSpawner;
    private FruitSpawner fruitSpawner;

    public float speed; // 현재 몬스터 속도
    float slowPercent; // 슬로우 크기
    int knockbackPower; // 넉백 차워
    float[] debuffCheckTimer; // 디버트 시간 체크용 배열
    bool[] debuffCheck; // 디버트 활성화 확인용 배열

    Canvas hpCanvas; // hp UI가 표시될 캔버스
    public GameObject hpBarPrefab; // hpBar 프리팹
    GameObject hpBar; // 설치된 본인의 hp 정보
    Slider hpSlider; // hp 슬라이더 정보
    MonsterUIManager hpBarManager; // hp bar의 컴포넌트
    float curHp;
    private HPManager hPManager;

    public GameObject slowEffact; // 슬로우 시 효과
    public GameObject bondageEffact; // 속박 시 효과

    private void Awake()
    {
        fruitSpawner = GetComponent<FruitSpawner>();
        waveSpawner = GameObject.Find("GameManager(1)").GetComponent<WaveSpawner>();
    }

    public void StartWayPoint(int waypoint)
    {
        Waypoint = waypoint;

        switch (Waypoint)
        {
            case 0:
                target = OneWaypoints.opoints[0];//첫번째 OneWaypoint 설정
                break;
            case 1:
                target = TwoWaypoints.tpoints[0];//두번째 twoWaypoint 설정
                break;
        }

        speed = defaultSpeed; // 기본 속도 설정
        debuffCheckTimer = new float[3] { 0, 0, 0 }; // 
        debuffCheck = new bool[3] { false, false, false };

        hpCanvas = GameObject.Find("MonsterHPCanvas").GetComponent<Canvas>(); // 게임내 캔버스 정보 가져오기
        hpBar = Instantiate<GameObject>(hpBarPrefab, hpCanvas.transform); // hpbar 배치
        hpSlider = hpBar.GetComponent<Slider>();
        hpBarManager = hpBar.GetComponent<MonsterUIManager>();

        hpBarManager.HpBarSetInMonster(this); // 소환된 hpbar의 정보 가져오기
        currentHp = maxHp;

        E1_rigidbody = GetComponent<Rigidbody>();
        FixedUpdate();
    }
    
    private void Update()
    {
        DebuffCheck(); // 매 프레임마다 디버프 체크

        if (debuffCheck[0]) // 슬로우 상태일 경우 슬로우 실행
        {
            Slow();
            slowEffact.SetActive(true);
        }
        else
        {
            slowEffact.SetActive(false);
        }
        
        if (debuffCheck[1]) // 속박 상태일 경우 속박 실행
        {
            Bondage();
            bondageEffact.SetActive(true);
        }
        else
        {
            bondageEffact.SetActive(false);
        }

        if (currentHp > 0)
        {
            //hpSlider.value = Mathf.Lerp(hpSlider.value, (float)currentHp / (float)maxHp, Time.deltaTime * 10);
        }
    }

    private void FixedUpdate()
    {
        Vector3 dir = target.position - transform.position;// 목적지 방향을 구하는 식
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);//이동관련 함수

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)//Vector3 a(transform.position)와 Vector3 b(target.position)의 사이의 거리가 0.4f보다 낮으면...
        {
            GetNextWaypoint();//다음 목적지 탐색하는 함수
        }

        Quaternion newRotation = target.rotation;
        E1_rigidbody.rotation = Quaternion.Slerp(E1_rigidbody.rotation, newRotation,
            rotatespeed * Time.deltaTime);
        //몬스터가 이동하는 방향으로 회전(바라봄)
    }

    private void GetNextWaypoint()
    {
        if (Waypoint == 0)
        {
            if (wavepointIndex >= OneWaypoints.opoints.Length - 1)//현재 목적지(wavepointIndex)가 마지막 목적지(Waypoints.points.Length -1)이라면
            {
                waveSpawner.DestroyEnemy(this); // 이 스크랩트를 가지고 있는 게임 객체를 파괴
                return;
            }

            wavepointIndex++;
            target = OneWaypoints.opoints[wavepointIndex];//목적지를 다음 목적지로 대입
        }
        else if (Waypoint == 1)
        {
            if (wavepointIndex >= TwoWaypoints.tpoints.Length - 1)//현재 목적지(wavepointIndex)가 마지막 목적지(Waypoints.points.Length -1)이라면
            {
                waveSpawner.DestroyEnemy(this); // 이 스크랩트를 가지고 있는 게임 객체를 파괴
                return;
            }
            wavepointIndex++;
            target = TwoWaypoints.tpoints[wavepointIndex];//목적지를 다음 목적지로 대입
        }
    }


    public void Damage(int damage)// 적 체력 깎는 함수
    {
        currentHp -= damage;
        hpSlider.value = (float)currentHp / (float)maxHp;

        // 기존의 Die Check 함수 내용을 Damage 안으로 이동
        if (currentHp <= 0)//적이 피가 0 이하면
        {
            waveSpawner.EnemyList_1.Remove(this);
            fruitSpawner.SpawnFruit();//열매 소환
            Destroy(this.gameObject);//몬스터 삭제
        }
    }

    // 슬로우 디버프
    public void SlowDebuff(float slowPercent_, int slowTime)
    {
        debuffCheckTimer[0] = slowTime; // 슬로우 시간 적용
        debuffCheck[0] = true; // 슬로우 상태 켜기
        slowPercent = slowPercent_; // 슬로우 퍼샌트 저장
    }

    // 슬로우 실행
    void Slow()
    {
        if (slowPercent <= 0) // 슬로우 퍼샌트가 0 보다 작을 경우
        {
            return;
        }

        if (speed < defaultSpeed || speed <= 0) // 이미 속도저하 디버프가 걸려 있는 경우
        {
            return;
        }

        speed = speed - speed * (slowPercent/100); // 슬로우 퍼샌트 만큼 적용
    }

    // 속박 디버프
    public void BondageDebuff(float stopTime)
    {
        debuffCheckTimer[1] = stopTime; // 속박 시간 적용
        debuffCheck[1] = true; // 속박 상태 켜기
    }

    // 속박 실행
    void Bondage()
    {
        if (speed <= 0) // 이미 속도가 0이거나 보다 작은 경우
        {
            return;
        }

        speed = 0; // 속도를 0으로 변경
    }

    // 넉백 디버프
    public void KnockBackDebuff(Vector3 bulletPosition, int knockBackPower)
    {
        // 탄환 위치와 적 위치를 통해 날아갈 방향 계산
        Vector3 knockbackPosition = transform.position - bulletPosition;
        knockbackPosition = knockbackPosition.normalized;

        // 방향에 힘을 주어 날리기 실행
        E1_rigidbody.AddForce(knockbackPosition * knockBackPower, ForceMode.Impulse);
        //transform.Translate(knockbackPosition * knockBackPower);
    }

    // 현재 디버프 상태와 시간을 체크하는 함수
    public void DebuffCheck()
    {
        // 디버프가 하나도 켜져있지 않은 경우
        if (!debuffCheck[0] && !debuffCheck[1] && !debuffCheck[2])
        {
            speed = defaultSpeed; // 기본 속도를 대입
            return;
        }

        // 디버프 상태를 체크
        for (int i = 0; i < debuffCheck.Length; i++)
        {
            if (!debuffCheck[i]) // 디버프가 켜져있지 않은 경우
            {
                continue;
            }

            // 디버트 시간 체크 배열에서 지나간 시간만큼 계속 빼기
            debuffCheckTimer[i] -= Time.deltaTime;

            // 디버프 시간이 0이거나 0일 경우 디버프 해제
            if (debuffCheckTimer[i] <= 0)
            {
                debuffCheckTimer[i] = 0;
                debuffCheck[i] = false;
            }
        }
    }
    //몬스터가 데스존에 부딫이면, 몬스터가 삭제됨
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            waveSpawner.EnemyList_1.Remove(this);
            Destroy(this.gameObject);//몬스터 삭제
            return;
        }
    }
}