using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家攻击
/// <summary>
public class PlayerAttack : MonoBehaviour
{
    // 发射子弹间距
    public float shootInterval = 0.15f;
    private float addTime = 0;
    // 枪光线间隔
    public float gunLightInterval = 0.05f;
    public int attack = 50;

    private AudioSource gunAudioSource;
    private Ray gunRay;
    private RaycastHit gunHit;
    // 枪口光源
    private Light gunLight;
    // 机枪光线
    private LineRenderer gunLine;
    private LayerMask layerMask;
    // 粒子特效
    private ParticleSystem gunParticle;
    private EnemyHealth enemyHealth;

    private void shoot()
    {
        // 播放音效
        gunAudioSource.Play();
        // 枪口火焰
        gunLight.enabled = true;
        // 弹道轨迹
        gunLine.enabled = true;
        // 起始方向
        gunRay.origin = this.transform.position;
        // 光线方向
        gunRay.direction = this.transform.forward;
        // 粒子特效
        gunParticle.Stop();
        gunParticle.Play();

        // gunLine的第一个点
        gunLine.SetPosition(0, this.transform.position);

        // 判断是否射到场景的游戏对象（layerMask）
        if (Physics.Raycast(gunRay,out gunHit, 100f, layerMask))
        {
            enemyHealth = gunHit.collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.onAttack(attack, gunHit.point);
            }
            // 光线的第二个点设置到打到的游戏对象上
            gunLine.SetPosition(1, gunHit.point);
            // 是否打到敌人

        }
        else
        {
            // 光线的第二个点设置到该方向上很远处
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
        // 鼠标左键开枪
        if(Input.GetButton("Fire1") && addTime > shootInterval)
        {
            // 执行射击
            shoot();
            addTime = 0;
        }
        if(addTime > gunLightInterval * shootInterval)
        {
            // 关闭弹道轨迹以及枪口火焰
            disableGun();
        }
    }

   
    
}
