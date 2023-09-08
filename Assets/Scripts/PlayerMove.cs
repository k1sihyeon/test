using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 이동 속도
    public float speed = 5;
    // CharacterController 컴포넌트
    CharacterController cc;
    // 중력 가속도의 크기
    public float gravity = -20;
    // 수직 속도
    float yVelocity = 0;
    // 점프 크기
    public float jumpPower = 5;
    int hp = 100;

    // 회전 관련 변수
    public float rotationSpeed = 5f;  // 회전 속도
    private Vector3 moveDirection;  // 이동 방향

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 사용자의 입력에 따라 전후좌우로 이동하고 싶다.
        // 1. 사용자의 입력을 받는다.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 2. 방향을 만든다.
        Vector3 dir = new Vector3(h, 0, v);
        // 2.0 사용자가 바라보는 방향으로 입력 값 변화시키기
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();

        // 2.1 중력을 적용한 수직 방향 추가 v = v0 + at
        yVelocity += gravity * Time.deltaTime;
        // 2.2 바닥에 있을 경우, 수직 항력을 처리하기 위해 속도를 0으로 한다.
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }
        // 2.3 사용자가 점프 버튼을 누르면 속도에 점프 크기를 할당한다.
        if (Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
        }

        // 3. 이동한다.
        Vector3 moveVelocity = dir * speed;
        moveVelocity.y = yVelocity;
        cc.Move(moveVelocity * Time.deltaTime);

        // 4. 회전한다.
        if (dir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void DamageAction(int damage)
    {
        hp -= damage;
    }
}
