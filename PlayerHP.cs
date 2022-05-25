using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//UI를 새로 추가하자.


//태어날 때 체력을 최대체력으로
//적이 플레이어를 공격할 때 체력을 감소하고싶다.
// 체력이 변경되면 UI에도 반영하고싶다.

public class PlayerHP : MonoBehaviour
{
    int hp;
    public int maxHP = 3;
    public Slider sliderHP;

    //property 사용
    public int HP
    //property는 get과 set이 반드시 있어야하고 절차적으로 진행함
    {
        get {
            return hp;
            //GetHP는 hp값 그 자체를 반환한다.
            }
        set {
            hp = value;
            sliderHP.value = value;
            }
    }


    public void AddDamage()
    {
        HP -= 1;
        //AddDamage 를 하면 SetHP로 기존의 HP(GetHP)에서 -1 으로 만들어주는 함수를 실행함
    }


    // Start is called before the first frame update
    void Start()
    {
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
