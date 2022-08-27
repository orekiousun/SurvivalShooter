using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 敌人生命值
/// <summary>
public class EnemyHealth : MonoBehaviour
{
    // 最大生命值
    public int maxHealth = 100;
    // 当前生命值
    public int currentHealth;
    // 是否死亡标志
    private bool isDied = false;
    // 死亡沉陷
    private bool isSinking = false;
    // 击败敌人后添加的分数
    public int addScore = 10;

    private AudioSource enemyAudioSource;
    private Animator enemyAnimator;
    public AudioClip audioClip;
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private CapsuleCollider capsuleCollider;
    private Rigidbody enemyRigidbody;
    private ParticleSystem enemyParticleSystem;


    public void onAttack(int damage, Vector3 hitPos)
    {
        if(isDied)
        {
            return;
        }
        else
        {
            // 减血
            currentHealth -= damage;
            // 播放音乐
            enemyAudioSource.Play();
            // 播放粒子动画
            enemyParticleSystem.transform.position = hitPos;
            enemyParticleSystem.Play();
        }
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        isDied = true;
        enemyAnimator.SetTrigger("Die");
        // 播放死亡语音
        enemyAudioSource.clip = audioClip;
        enemyAudioSource.Play();
        
        // 禁用攻击组件
        enemyAttack.enabled = false;
        // 移动组件已在EnemyMovement中定义
        // 使碰撞器失效
        capsuleCollider.isTrigger = true;
        enemyRigidbody.isKinematic = true;
    }

    // Animation中定义的动画事件
    public void StartSinking()
    {
        this.GetComponent<NavMeshAgent>().enabled = false;
        isSinking = true;
        ScoreManager.score += addScore;
        // 移除游戏对象
        Destroy(gameObject, 1.5f);
    }

    void Start()
    {
        currentHealth = maxHealth;
        enemyAudioSource = this.GetComponent<AudioSource>();
        enemyAnimator = this.GetComponent<Animator>();
        enemyMovement = this.GetComponent<EnemyMovement>();
        enemyAttack = this.GetComponent<EnemyAttack>();
        capsuleCollider = this.GetComponent<CapsuleCollider>();
        enemyRigidbody = this.GetComponent<Rigidbody>();
        enemyParticleSystem = this.GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(Vector3.up * -2f * Time.deltaTime);
        }
    }
}
