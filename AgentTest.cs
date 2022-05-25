using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//nav mesh agent를 사용하려면 AI 를 반드시 추가해야함 

public class AgentTest : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
    }
}
