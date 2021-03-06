using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//nav mesh agnet 를 사용하기 위해서는 반드시 불러와야함


//FSM(finite-state machine:유한상태머신) 으로 상태를 제어하고싶다.


public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;



    //NavMeshAgnet 로 agent 선언

    public enum State
        //enum은 변수의 콘테이너이다. "열거형"이며 type 도 정해줄수있다.
    {
        // 마우스를 올려보면 각각 State마다 숫자가 지정되어있는걸 확인 할 수 있다.
        Idle,
        Move,
        Attack,
        Die
    }

    public Animator anim;
    //좀비 프리팹을 넣을 수 있는 공간 확보


    public State state;
    PlayerHP php;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent에 컴포넌트 장착

        state = State.Idle;

        target = GameObject.Find("Player");
        php = target.GetComponent<PlayerHP>();
        //플레이어를 공격하기 위해 PlayerHP 값을 갖고온다.
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Idle)
        {
            UpdateIdle();
            //만들어진 State를 자동으로 만드려면 Alt + enter 를 누르면 자동 만들어짐이됨
        }else if(state == State.Move)
        {
            UpdateMove();
        }
    }

    public float findDistance = 5;
    //탐지거리는 5로 기본으로 잡자
    private void UpdateIdle()
    {
        //target이 감지거리안에 들어오면 Move로 전이하고 싶다.
        //1. 나와 target의 거리를 구해서
        float distance = Vector3.Distance(transform.position, target.transform.position);
        //내 위치와 타겟의 위치 Distance 구하기

        //2. 만약 그 거리가 감지거리보다 작으면
        if(distance < findDistance)
        {
            //3. Move상태로 전이하고싶다.
            state = State.Move;

            anim.SetTrigger("Move");
            //animator 패널 안에 있었던 Trigger를 Set하고 parameter에서 지정한 Move Trigger 지정하기

            agent.destination = target.transform.position;

        }

    }

    public float speed = 1;
    public float attackDistance = 1.5f; 
    private void UpdateMove()
    {
        //target 방향으로 이동하다가 target이 공격거리안에 들어오면 Attack으로 전이하고싶다.

        ////1. target 방향으로 이동하고 싶다. (직진이동)
        //Vector3 dir = target.transform.position - transform.position;
        //// 내쪽 방향으로 오게하고싶으면 target의 위치 - 나의 위치 로 하면된다.
        //dir.Normalize();
        ////방향정규화
        //transform.position += dir * speed * Time.deltaTime;
        ////이동속도 설정

        
        //1. agent 컴포넌트를 이용해서 destination 지정.
        agent.destination = target.transform.position;


        //2. 나와 target의 거리를 구해서
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //3. 만약 그 거리가 공격거리보다 작으면
        if (distance < attackDistance)
        {
            anim.SetTrigger("Attack");
            //animator 패널 안에 있었던 Trigger를 Set하고 parameter에서 지정한 Attack Trigger 지정하기

            //4. Attack상태로 전이하고싶다.
            state = State.Attack;
            agent.isStopped = true;
            // 어택 상황이되면 agent가 멈추도록
        }

    }

    internal void OnEventAttack()
    //internal은 public 과 같은 접근 제한자이다. 같은 프로그램 안에서만 public을 활성화 하는 역할 (보안상 이유)
    {
        //공격 행위

        //4. 플레이어를 공격하고
        //target.AddDamage();

        //5. 만약 target이 공격거리 밖에 있으면 Move상태로 전이하고 싶다. (플레이어가 도망갔을 경우)
        float distance = Vector3.Distance(transform.position, target.transform.position);
        // 5-1. 만약 나와 target의 거리를 구했는데 그 거리가 공격거리보다 크면
        if (distance > attackDistance)
        {
            anim.SetTrigger("Move");
            //animator 패널 안에 있었던 Trigger를 Set하고 parameter에서 지정한 Move Trigger 지정하기

            // 5-2. Move 상태로 전이.
            state = State.Move;
            agent.isStopped = false;
            // 멀어진다면 agnet 스탑
        }
        else
        {
            //공격 성공
            //4. 플레이어를 공격하고
            php.AddDamage();
            //target.AddDamage();
            //HitManager.cs의 Hit함수를 호출하고 싶다.
            //HitManager hm = GameObject.Find("HitManager").GetComponent<HitManager>();
            //hm.Hit();

            HitManager.instance.Hit();
            // HitManager의 this와 instance를 동일시했기에 HitManager를 찾는 방법이 간소화 됐다.
        }
    }


    float currentTime;
    float attackTime = 1;
    private void UpdateAttack()
    {
        

        //일정시간마다 공격을 하되, 공격시점에 target이 공격거리 밖에 있으면 Move상태로 전이하고싶다.
        //1. 시간이 흐르다가
        currentTime += Time.deltaTime;

        //2. 현재시간이 공격시간이 되면
        if(currentTime > attackTime)
        {
            //3. 현재시간을 초기화하고
            currentTime = 0;

        }

    }

    public void AddDamage(int damage)
    {
        state = State.Die;
        anim.SetTrigger("Die");

        Destroy(gameObject,3f);
        // Destroy의 2번째 arg는 죽고나서 없어지는 딜레이 타임을 넣을 수 있다.
    }

    private void OnDestroy()
        //Destroy 가 실행됐을때 일어나는 함수 OnDestroy
    {
        EnemyManager.instance.COUNT--;
        //EnemyManager 의 COUNT property 에서 감소
    }

}
