using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ray 를 이용해서 바라보고 닿은곳에 총을 쏘고 싶다.
//총알 자국을 남기고 싶다.

public class Gun : MonoBehaviour
{
    public ParticleSystem bulletImpact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //0. 만약 마우스 왼쪽 버튼을 누르면
        if (Input.GetButtonDown("Fire1"))
        {
            //1. 시선을 만들고
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            //Ray(원점, 방향)

            //2. 그 시선을 이용해서 바라봤는데 만약 닿는곳이 있다면?
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                //3. 닿은곳의 정보를 출력하고 싶다.
                print(hitInfo.transform.name);
                // 히트한 곳의 정보중에 name을 출력하고싶다.

                //4. 닿은곳에 총알자국을 가져다 놓고싶다.
                bulletImpact.transform.position = hitInfo.point;
                //point를 이용해서 hitInfo의 중앙점을 잡는다.

                //5. 총알자국(VFX)를 재사용 대기시켜놓자.
                bulletImpact.Stop();
                bulletImpact.Play();

                //6. 총알 자국을 닿은곳의 normal 방향으로 rotate하고싶다.
                //   총알자국의 forward 방향과 normal방향과 일치시키고 싶다.
                bulletImpact.transform.forward = hitInfo.normal;

                //Enemy.cs와 연동
                //만약 hitInfo가 Enemy컴포넌트를 갖고있다면?
                Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // enemy의 AddDamage함수를 호출하고싶다.
                    enemy.AddDamage(1);

                }

            }
        }

        
    }
}
