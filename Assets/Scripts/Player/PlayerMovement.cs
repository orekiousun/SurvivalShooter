using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��������ƶ�
/// <summary>
public class PlayerMovement : MonoBehaviour
{
    // ��Ҹ������
    private Rigidbody plyerRigidbody;
    // ��ȡFloor��
    public LayerMask floorMask;
    // ��ȡAnimator���
    private Animator playerAction;
    
    // ��ȡ����ˮƽ�ʹ�ֱ����
    private float hor;
    private float ver;
    // ��ɫ�ƶ��ٶ�
    public int moveSpeed = 6;
    // ��������ƶ�λ��
    private Vector3 movement;
    
    private void move()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
        movement.Set(hor, 0f, ver);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        if (hor != 0 || ver != 0)
        {
            plyerRigidbody.MovePosition(transform.position + movement);
        }
    }

    private void animating()
    {
        bool walking = hor != 0 || ver != 0;
        playerAction.SetBool("IsWalking", walking);
    }

    private void turning()
    {
        // ͨ���������ǰ�������Ļ�е�λ�÷���һ������
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit cameraHit;
        // ���������Ƿ��䵽�ذ�
        // �����������ת��Χ�̶��ڵذ��ڣ�cameraHit�����䵽��λ��
        if (Physics.Raycast(cameraRay,out cameraHit, 100f, floorMask))
        {
            Vector3 playerToMouse = cameraHit.point - transform.position;
            // playerToMouse.y = 0f;   ���Բ���Ҫ��Rigidbody���Ѿ�������y����ת
            Quaternion newQuaternion = Quaternion.LookRotation(playerToMouse);
            plyerRigidbody.MoveRotation(newQuaternion);
        }
    }

    private void Awake()
    {
        plyerRigidbody = this.GetComponent<Rigidbody>();
        playerAction = this.GetComponent<Animator>();
        floorMask = LayerMask.GetMask("Floor");
    }

    // ʹ��FixedUpdate�̶�ʱ�����
    private void FixedUpdate()
    {
        // �ƶ�
        move();

        // ��⶯��������״̬�л�
        animating();

        // ������ת
        turning();
    }

}
