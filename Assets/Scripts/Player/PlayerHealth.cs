using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///
/// <summary>
public class PlayerHealth : MonoBehaviour
{
    // 当前生命值
    public float maxHealth = 100;
    // 最大生命值
    public float currentHealth;
    // 是否受伤
    private bool isDamage;
    // 受击颜色
    public Color hurtColor = new Color(1f, 0f, 0f, 0.1f);
    // 受击时闪屏播放间隔
    public float hurtInterval = 0.1f;
    private float addTime = 0;
    // Lerp中使用的平滑数值
    public float smoothing = 180;

    // 血条
    public Slider slider;
    // 受击全屏动画
    public Image hurt;
    // 音效
    private AudioSource playerAudio;
    // 动画组件
    private Animator playerAction;
    // 死亡音效
    public AudioClip deadClip;
    // 获取PlayerMovement移动脚本
    private PlayerMovement playerMovement;
    // 人物攻击模块
    private PlayerAttack playerAttack;
    

    public void onAttack(float damage)
    {
        isDamage = true;
        if(currentHealth >= 0)
        {
            // 掉血
            currentHealth = currentHealth - damage;
            // 更新UI
            slider.value = currentHealth;
            // 受击音效
            playerAudio.Play();
            if(currentHealth <= 0)
            {
                playerAction.SetTrigger("Die");
                playerAudio.clip = deadClip;
                playerAudio.Play();
                // 禁用PlayerMovement移动脚本
                playerMovement.enabled = false;
                playerAttack.enabled = false;
            }
        }
    }



    void Start()
    {
        playerAudio = this.GetComponent<AudioSource>();
        playerAction = this.GetComponent<Animator>();
        playerMovement = this.GetComponent<PlayerMovement>();
        playerAttack = this.GetComponentInChildren<PlayerAttack>();
        currentHealth = maxHealth;
        slider.value = currentHealth;
    }

    void Update()
    {
        if (isDamage)
        {
            hurt.GetComponent<Image>().color = Color.Lerp(hurt.GetComponent<Image>().color, hurtColor, 0.1f * Time.deltaTime * smoothing);
            addTime = addTime + Time.deltaTime;
            if (addTime > hurtInterval)
            {
                addTime = 0;
                isDamage = false;
            }
        }
        else
        {
            hurt.GetComponent<Image>().color = Color.clear;
        }

    }

}
