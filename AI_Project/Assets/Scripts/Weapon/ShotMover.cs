﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMover : MonoBehaviour
{
    public GameObject Explosion;
    public float Speed;
    public float LifeTime;
    public float Power = 1;

    private Rigidbody rb;
    private string ShooterName;
    private string OtherSameShooterBullet;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Speed;
        Destroy(gameObject, LifeTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (ShooterName == "") Debug.Log("ShooterName is null");
        if (col.tag.Equals("Bullet"))
        {
            if (!col.GetComponent<ShotMover>().GetShooterName().Equals(ShooterName))
            {
                Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
        }
        else if (col.name != ShooterName && !col.tag.Equals("Enemy Eyeshot"))
        {
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    public void SetShooterName(string s)
    {
        ShooterName = s;
    }

    public string GetShooterName()
    {
        return ShooterName;
    }
}