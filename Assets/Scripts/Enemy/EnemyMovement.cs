using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
///
/// <summary>
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent nav;
    private EnemyHealth enemyHealth;
    private PlayerHealth playerHealth;
    private Animator anim;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        nav = this.GetComponent<NavMeshAgent>();
        enemyHealth = this.GetComponent<EnemyHealth>();
        playerHealth = target.GetComponent<PlayerHealth>();
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            nav.SetDestination(target.position);
        else
        {
            nav.enabled = false;
            // ÉèÖÃ¶¯×÷
            anim.SetTrigger("PlayerDie");
        }
    }
}
