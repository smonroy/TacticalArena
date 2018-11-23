using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProperties : MonoBehaviour
{
    int health;
    public int fullHealth = 3;

    public bool invincible { get; private set; }

    public float invincibleDelay = 2;
    float invincibleTimer;

    public bool on = true;
    public float flashDelay = 0.1f;
    float flashTimer;
    Material material;
    Color matColor;

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            int prevHealth = health;
            health = value;
            if (health < 1)
            {
                Destroy(gameObject);
            }
            else if (health < prevHealth)
            {
                invincible = true;
                invincibleTimer = Time.time + invincibleDelay;
                flashTimer = Time.time + flashDelay;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        health = fullHealth;
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {
            if (Time.time > flashTimer)
            {
                matColor = material.color;
                if (on) {
                    matColor.a = 0;
                    on = false;
                }
                else {
                    matColor.a = 1;
                    on = true;
                }
                material.color = matColor;
            }
            if (Time.time > invincibleTimer)
            {
                invincible = false;
                matColor = material.color;
                matColor.a = 1;
                on = true;
                material.color = matColor;
            }
        }
    }
}
