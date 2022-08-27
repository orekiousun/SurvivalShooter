using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制玩家移动
/// <summary>
public class PlayerMovement : MonoBehaviour
{
    // 玩家刚体组件
    private Rigidbody plyerRigidbody;
    // 获取Floor层
    public LayerMask floorMask;
    // 获取Animator组件
    private Animator playerAction;
    
    // 获取键盘水平和垂直输入
    private float hor;
    private float ver;
    // 角色移动速度
    public int moveSpeed = 6;
    // 定义玩家移动位移
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
        // 通过摄像机向当前鼠标在屏幕中的位置发射一条射线
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit cameraHit;
        // 检测该射线是否射到地板
        // 即将摄像机旋转范围固定在地板内，cameraHit返回射到的位置
        if (Physics.Raycast(cameraRay,out cameraHit, 100f, floorMask))
        {
            Vector3 playerToMouse = cameraHit.point - transform.position;
            // playerToMouse.y = 0f;   可以不需要，Rigidbody中已经锁定了y轴旋转
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

    // 使用FixedUpdate固定时间更新
    private void FixedUpdate()
    {
        // 移动
        move();

        // 检测动画，动作状态切换
        animating();

        // 人物旋转
        turning();
    }

}
