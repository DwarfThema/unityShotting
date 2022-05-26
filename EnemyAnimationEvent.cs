using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//애니메이션 공격 동작에 호출할 이벤트 함수를 제작하고 싶다.
//이벤트가 발생하면 Enemy컴포넌트에게 알려주고 싶다.

public class EnemyAnimationEvent : MonoBehaviour
{
    public Enemy enemy;
    //에너미와 연결시켜줄 수 있는 공간

    public void OnEventAttack()
    {
        print("어택!");
        enemy.OnEventAttack();
        //alt + enter 를 눌러서 Function 생성 / F12를 누르면 Function으로 이동
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
