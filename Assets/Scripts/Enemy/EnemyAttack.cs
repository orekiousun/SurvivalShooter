using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///
/// <summary>
public class EnemyAttack : MonoBehaviour
{
    // 攻击力
    public float damage = 5f;
    // 攻击时间间距
    public float attackInterval = 1f;
    // 累计时间值
    private float addTime = 0;
    // 是否进入攻击范围
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
        // 玩家进入敌人攻击范围
        if (other.gameObject == player)
        {
            // 在范围的标志为true
            isInrange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 玩家离开敌人攻击范围
        if (other.gameObject == player)
        {
            // 在范围的标志为true
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
