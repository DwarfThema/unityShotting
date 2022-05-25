using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    //태어날 때 imageHit를 보이지 않게 하고싶다.
    // 적이 플레이어를 공격했을 때 imageHit를 깜빡이고 싶다.


    public static HitManager instance;
    // static을 넣으면 처음부터 class에서 static하게 존재하고있는 객체임 (지금은 HitManager것) = 싱글톤화 시킨다.

    private void Awake()
    //Awake는 Start 보다 더 먼저 실행되는 함수
    {
        instance = this;
        //instance = this 를 하면 HitManager는 instance와 동일해 진다.
        //이를 활용해서 전역으로 HitManager를 사용할 수 있다. (Enemy.cs 공격 성공 파트에서 활용해보자)
    }

    public GameObject imageHit;


    // Start is called before the first frame update
    void Start()
    {
        //태어날 때 imageHit를 보이지 않게 하고싶다.
        this.imageHit.SetActive(false);
        //위 this는 원래 생략되어있는 영역임
    }

    public void Hit()
    {
        //깜빡거리고 싶다.
        StartCoroutine("IeHit");
    }

    IEnumerator IeHit()
    {
        //1. imageHit를 보이게 하고싶다.
        imageHit.SetActive(true);
        //2. 0.1초 후에
        yield return new WaitForSeconds(0.1f);
        //3. imageHit를 안보이게 하고싶다.
        imageHit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) 
        {
            Hit();
        }
    }
}
