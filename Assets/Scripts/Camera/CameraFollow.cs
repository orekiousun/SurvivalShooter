using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���������
/// <summary>
public class CameraFollow : MonoBehaviour
{
    // ��ȡ���Transform���
    public Transform plyerPosition;
    // ƽ��ֵ
    public float smoothing = 5f;
    // �����ֵ
    private Vector3 offset;

    void Start()
    {
        offset = this.transform.position - plyerPosition.position;
    }
    
    void Update()
    {
        // ʵʱ�ƶ�����
        Vector3 targetPosition = plyerPosition.position + offset;
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
