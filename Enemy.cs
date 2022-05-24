using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FSM(finite-state machine:유한상태머신) 으로 상태를 제어하고싶다.


public class Enemy : MonoBehaviour
{
    public enum State
        //enum은 변수의 콘테이너이다. "열거형"이며 type 도 정해줄수있다.
    {
        // 마우스를 올려보면 각각 State마다 숫자가 지정되어있는걸 확인 할 수 있다.
        Idle,
        Move,
        Attack,
        Die
    }

    public State state;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;

        target = GameObject.Find("Player");
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
        }else if(state == State.Attack)
        {
            UpdateAttack();
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
        }

    }

    public float speed = 1;
    public float attackDistance = 1.5f; 
    private void UpdateMove()
    {
        //target 방향으로 이동하다가 target이 공격거리안에 들어오면 Attack으로 전이하고싶다.
        //1. target 방향으로 이동하고 싶다.
        Vector3 dir = target.transform.position - transform.position;
        // 내쪽 방향으로 오게하고싶으면 target의 위치 - 나의 위치 로 하면된다.
        dir.Normalize();
        //방향정규화

        transform.position += dir * speed * Time.deltaTime;
        //이동속도 설정

        //2. 나와 target의 거리를 구해서
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //3. 만약 그 거리가 공격거리보다 작으면
        if (distance < attackDistance)
        {
            //4. Attack상태로 전이하고싶다.
            state = State.Attack;
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

            //4. 플레이어를 공격하고
            //target.AddDamage();

            //5. 만약 target이 공격거리 밖에 있으면 Move상태로 전이하고 싶다. (플레이어가 도망갔을 경우)
            float distance = Vector3.Distance(transform.position, target.transform.position);
            // 5-1. 만약 나와 target의 거리를 구했는데 그 거리가 공격거리보다 크면
            if (distance > attackDistance)
            {
                // 5-2. Move 상태로 전이.
                state = State.Move;
            }

        }

    }

    public void AddDamage(int damage)
    {
        Destroy(gameObject);
    }

}
