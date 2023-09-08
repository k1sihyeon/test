using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �̵� �ӵ�
    public float speed = 5;
    // CharacterController ������Ʈ
    CharacterController cc;
    // �߷� ���ӵ��� ũ��
    public float gravity = -20;
    // ���� �ӵ�
    float yVelocity = 0;
    // ���� ũ��
    public float jumpPower = 5;
    int hp = 100;

    // ȸ�� ���� ����
    public float rotationSpeed = 5f;  // ȸ�� �ӵ�
    private Vector3 moveDirection;  // �̵� ����

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ������� �Է¿� ���� �����¿�� �̵��ϰ� �ʹ�.
        // 1. ������� �Է��� �޴´�.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 2. ������ �����.
        Vector3 dir = new Vector3(h, 0, v);
        // 2.0 ����ڰ� �ٶ󺸴� �������� �Է� �� ��ȭ��Ű��
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();

        // 2.1 �߷��� ������ ���� ���� �߰� v = v0 + at
        yVelocity += gravity * Time.deltaTime;
        // 2.2 �ٴڿ� ���� ���, ���� �׷��� ó���ϱ� ���� �ӵ��� 0���� �Ѵ�.
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }
        // 2.3 ����ڰ� ���� ��ư�� ������ �ӵ��� ���� ũ�⸦ �Ҵ��Ѵ�.
        if (Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
        }

        // 3. �̵��Ѵ�.
        Vector3 moveVelocity = dir * speed;
        moveVelocity.y = yVelocity;
        cc.Move(moveVelocity * Time.deltaTime);

        // 4. ȸ���Ѵ�.
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
