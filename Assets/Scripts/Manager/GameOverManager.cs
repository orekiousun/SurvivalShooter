using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///
/// <summary>
public class GameOverManager : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Animator anim;
    public float restartTime = 5;
    private float addTime;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        anim = this.GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            addTime += Time.deltaTime;
            if (addTime > restartTime)
            {
                ScoreManager.score = 0;
                addTime = 0;
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}
