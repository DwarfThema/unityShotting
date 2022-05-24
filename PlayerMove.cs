using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
    public float gravity = -9.81f;
    public float jumpPower = 10;
    float yVelocity;

    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        yVelocity += gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.Normalize();

        dir = Camera.main.transform.TransformDirection(dir);

        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);

    }
}
