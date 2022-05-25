using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일정시간마다 적 공장에 적을 생성해서 내 위치에 가져다 놓고싶다.
//에너미가 죽었을 때 count 갯수를 감소시켜야 한다.
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    //instance 지정을 해서 싱글톤화 시켜주자.

    private void Awake()
    {
        instance = this;
    }


    public GameObject enemyFactory;
    public float createTime = 2;

    int count;
    public int COUNT
    // property 를 이용해서 int count 에 직접 접근하지 않고 property를 활용 간접 접근하도록 하자.
    {
        get { return count; }
        set { 
            count = value;
            
            if(count < 0)
            {
                count = 0;
            }
            if(count > maxCount)
            {
                maxCount = count;
            }

            }
        // 이제 enemy.cs 로 가서 OnDestroy 에서 함수를 할당하자.
    }


    public int maxCount = 2;
    //최대 카운트를 정해서 에너미 갯수를 조절하자.

    // Start is called before the first frame update
    IEnumerator Start()
    //start를 코루틴으로 변경 반드시 yield 가 있어야 오류가 사라진다.
    {
        while (true)
        {   
            if(count < maxCount)
                //수가 maxCount 보다 작을때만 생성하는걸로
            {
                //1. 적공장에서 적을 생성
                GameObject enemy = Instantiate(enemyFactory);
                //instantiate 는 프리팹을 다시 게임오브젝트로 바꿔준다.

                //2. 내 위치로 이동하게 만들고 싶다.
                enemy.transform.position = this.transform.position + new Vector3(Random.value *2, 0, Random.value * 2);
                //this 는 생략이 된다.
                //Vector3 를 더함으로써 주변 랜덤값으로 생성된다. (*2 를 했으니 2미터만큼)

                //3. 내 방향과 일치시키고 싶다.
                enemy.transform.rotation = this.transform.rotation;
                count++;

            }

            //4. 생성시간동안 대기하고싶다.
            yield return new WaitForSeconds(createTime);
            //WaitForSeconds 로 생성 시간 조절을 하자.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
