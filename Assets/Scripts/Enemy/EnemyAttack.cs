using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///
/// <summary>
public class EnemyAttack : MonoBehaviour
{
    // ������
    public float damage = 5f;
    // ����ʱ����
    public float attackInterval = 1f;
    // �ۼ�ʱ��ֵ
    private float addTime = 0;
    // �Ƿ���빥����Χ
    private bool isInrange;

    private PlayerHealth playerHealth;
    private GameObject player;
    private Animator enemyAnimator;
    

    private void enemyAttack()
    {
        if (playerHealth.currentHealth > 0)
            playerHealth.onAttack(damage);
        else
            enemyAnimator.SetTrigger("PlayerDie");
    }

    private void OnTriggerEnter(Collider other)
    {
        // ��ҽ�����˹�����Χ
        if (other.gameObject == player)
        {
            // �ڷ�Χ�ı�־Ϊtrue
            isInrange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ����뿪���˹�����Χ
        if (other.gameObject == player)
        {
            // �ڷ�Χ�ı�־Ϊtrue
            isInrange = false;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {
        addTime = addTime + Time.deltaTime;
        if (isInrange && addTime > attackInterval)
        {
            addTime = 0;
            enemyAttack();
        }
    }
}
