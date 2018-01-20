﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public float DamageFromEnemyCollision = 5;
    public float DelayFromTakeDamage = 0.5f;

    //public float LifeTime;

    private float m_CurrentHealth;
    private bool m_Dead;
    private float lastHit;

    //private Rigidbody rb;
    private PlayerAnimatorController pAnimC;

    public float CurrentHealth
    {
        get { return m_CurrentHealth; }
    }

    private void Awake()
    {
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
        pAnimC = GetComponentInChildren<PlayerAnimatorController>();
        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("Player take damage");
        pAnimC.GetHit();
        m_CurrentHealth -= amount;
        SetHealthUI();
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        m_Slider.value = m_CurrentHealth;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        m_Dead = true;
        GameController.Instace.GameOver();
        //rb = GetComponent<Rigidbody>();
        //Destroy(gameObject, LifeTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        string colTag = collider.transform.tag;
        if (colTag.Equals("Enemy Weapon") && lastHit + DelayFromTakeDamage < Time.time && !m_Dead)
        {
            lastHit = Time.time;
            TakeDamage(collider.gameObject.GetComponent<ShotMover>().Power);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        string colTag = collision.transform.tag;
        if ((colTag.Equals("Enemy") || colTag.Equals("Boss")) && lastHit + DelayFromTakeDamage < Time.time && !m_Dead)
        {
            lastHit = Time.time;
            TakeDamage(DamageFromEnemyCollision);
        }
    }
}