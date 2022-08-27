using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// ��������ֵ
/// <summary>
public class EnemyHealth : MonoBehaviour
{
    // �������ֵ
    public int maxHealth = 100;
    // ��ǰ����ֵ
    public int currentHealth;
    // �Ƿ�������־
    private bool isDied = false;
    // ��������
    private bool isSinking = false;
    // ���ܵ��˺���ӵķ���
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
            // ��Ѫ
            currentHealth -= damage;
            // ��������
            enemyAudioSource.Play();
            // �������Ӷ���
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
        // ������������
        enemyAudioSource.clip = audioClip;
        enemyAudioSource.Play();
        
        // ���ù������
        enemyAttack.enabled = false;
        // �ƶ��������EnemyMovement�ж���
        // ʹ��ײ��ʧЧ
        capsuleCollider.isTrigger = true;
        enemyRigidbody.isKinematic = true;
    }

    // Animation�ж���Ķ����¼�
    public void StartSinking()
    {
        this.GetComponent<NavMeshAgent>().enabled = false;
        isSinking = true;
        ScoreManager.score += addScore;
        // �Ƴ���Ϸ����
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
