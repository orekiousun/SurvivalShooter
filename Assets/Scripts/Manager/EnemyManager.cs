using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��������
/// <summary>
public class EnemyManager : MonoBehaviour
{
    public float spawnTime = 6f;

    public GameObject enemy;
    public Transform[] spawnPoint;
    private PlayerHealth playerHealth;
  
    private void spawn()
    {
        if(playerHealth.currentHealth > 0)
        {
            int index = Random.Range(0, spawnPoint.Length);
            // ���ɵ���
            Instantiate(enemy, spawnPoint[index].position, spawnPoint[index].rotation);
        }
    }

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        InvokeRepeating("spawn", spawnTime, spawnTime);
    }

    void Update()
    {
        
    }
}
