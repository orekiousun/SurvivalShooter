using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 摄像机跟随
/// <summary>
public class CameraFollow : MonoBehaviour
{
    // 获取玩家Transform组件
    public Transform plyerPosition;
    // 平滑值
    public float smoothing = 5f;
    // 距离插值
    private Vector3 offset;

    void Start()
    {
        offset = this.transform.position - plyerPosition.position;
    }
    
    void Update()
    {
        // 实时移动距离
        Vector3 targetPosition = plyerPosition.position + offset;
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
