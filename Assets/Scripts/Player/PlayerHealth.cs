using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///
/// <summary>
public class PlayerHealth : MonoBehaviour
{
    // ��ǰ����ֵ
    public float maxHealth = 100;
    // �������ֵ
    public float currentHealth;
    // �Ƿ�����
    private bool isDamage;
    // �ܻ���ɫ
    public Color hurtColor = new Color(1f, 0f, 0f, 0.1f);
    // �ܻ�ʱ�������ż��
    public float hurtInterval = 0.1f;
    private float addTime = 0;
    // Lerp��ʹ�õ�ƽ����ֵ
    public float smoothing = 180;

    // Ѫ��
    public Slider slider;
    // �ܻ�ȫ������
    public Image hurt;
    // ��Ч
    private AudioSource playerAudio;
    // �������
    private Animator playerAction;
    // ������Ч
    public AudioClip deadClip;
    // ��ȡPlayerMovement�ƶ��ű�
    private PlayerMovement playerMovement;
    // ���﹥��ģ��
    private PlayerAttack playerAttack;
    

    public void onAttack(float damage)
    {
        isDamage = true;
        if(currentHealth >= 0)
        {
            // ��Ѫ
            currentHealth = currentHealth - damage;
            // ����UI
            slider.value = currentHealth;
            // �ܻ���Ч
            playerAudio.Play();
            if(currentHealth <= 0)
            {
                playerAction.SetTrigger("Die");
                playerAudio.clip = deadClip;
                playerAudio.Play();
                // ����PlayerMovement�ƶ��ű�
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
