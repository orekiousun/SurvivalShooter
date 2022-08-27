using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ҹ���
/// <summary>
public class PlayerAttack : MonoBehaviour
{
    // �����ӵ����
    public float shootInterval = 0.15f;
    private float addTime = 0;
    // ǹ���߼��
    public float gunLightInterval = 0.05f;
    public int attack = 50;

    private AudioSource gunAudioSource;
    private Ray gunRay;
    private RaycastHit gunHit;
    // ǹ�ڹ�Դ
    private Light gunLight;
    // ��ǹ����
    private LineRenderer gunLine;
    private LayerMask layerMask;
    // ������Ч
    private ParticleSystem gunParticle;
    private EnemyHealth enemyHealth;

    private void shoot()
    {
        // ������Ч
        gunAudioSource.Play();
        // ǹ�ڻ���
        gunLight.enabled = true;
        // �����켣
        gunLine.enabled = true;
        // ��ʼ����
        gunRay.origin = this.transform.position;
        // ���߷���
        gunRay.direction = this.transform.forward;
        // ������Ч
        gunParticle.Stop();
        gunParticle.Play();

        // gunLine�ĵ�һ����
        gunLine.SetPosition(0, this.transform.position);

        // �ж��Ƿ��䵽��������Ϸ����layerMask��
        if (Physics.Raycast(gunRay,out gunHit, 100f, layerMask))
        {
            enemyHealth = gunHit.collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.onAttack(attack, gunHit.point);
            }
            // ���ߵĵڶ��������õ��򵽵���Ϸ������
            gunLine.SetPosition(1, gunHit.point);
            // �Ƿ�򵽵���

        }
        else
        {
            // ���ߵĵڶ��������õ��÷����Ϻ�Զ��
            gunLine.SetPosition(1, gunRay.origin + gunRay.direction * 100f);
        }
    }

    void Start()
    {
        gunAudioSource = this.GetComponent<AudioSource>();
        gunLight = this.GetComponent<Light>();
        gunLine = this.GetComponent<LineRenderer>();
        layerMask = LayerMask.GetMask("Shootable");
        gunParticle = this.GetComponent<ParticleSystem>();
    }

    private void disableGun()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Update()
    {
        addTime = addTime + Time.deltaTime;
        // ��������ǹ
        if(Input.GetButton("Fire1") && addTime > shootInterval)
        {
            // ִ�����
            shoot();
            addTime = 0;
        }
        if(addTime > gunLightInterval * shootInterval)
        {
            // �رյ����켣�Լ�ǹ�ڻ���
            disableGun();
        }
    }

   
    
}
