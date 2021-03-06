﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostNova : MonoBehaviour
{
    // Start is called before the first frame update
    public float Range;
    public float DamageScale;
    public float TickRate;
    public float Duration;
    float Damage;
    float counter;
    bool Switch = false;
    SpriteRenderer SR;
    GameObject Player;
    void Start()
    {
        Damage = GameObject.FindGameObjectWithTag("Experience_Manager").GetComponent<Experience_Manager>().ReturnLevel() * DamageScale;
        SR = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Spell", 0f, TickRate);
        transform.localScale = new Vector3(Range, Range, 1f);
    }

    void Spell()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Damagable");
        for (int i = 0; i < Enemies.Length; i++)
        {
            try
            {
                if (Vector3.Distance(transform.position, Enemies[i].transform.position) < Range)
                {
                    Enemies[i].GetComponent<Enemy_Health>().Damage(Damage);
                }
            }
            catch
            {
                Debug.Log("Couldnt find Enemy Health component");
            }
        }
    }

    void Effect()
    {
        transform.position = Player.transform.position;
        switch (Switch)
        {
            case false:
                counter += Time.deltaTime*2;
                SR.color = Color.Lerp(Color.cyan,Color.clear, counter);
                if(counter >= 1)
                {
                    counter = 0;
                    Switch = true;
                }
                break;
            case true:
                counter += Time.deltaTime*2;
                SR.color = Color.Lerp(Color.clear, Color.cyan, counter);
                if (counter >= 1)
                {
                    counter = 0;
                    Switch = false;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Duration -= Time.deltaTime;
        Effect();
        if(Duration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
