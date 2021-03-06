﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Shear : MonoBehaviour
{
    // Start is called before the first frame update
    public float DamageScale;
    public float Speed;
    Vector3 pos;
    Vector3 StartPos;
    float Damage;
   
    void Start()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3(pos.x, pos.y, 0f);
        Damage = DamageScale * GameObject.FindGameObjectWithTag("Experience_Manager").GetComponent<Experience_Manager>().ReturnLevel();
       
        StartPos = transform.position;
        FixRotation();
        Spell();
    }

    void FixRotation()
    {
        Vector3 temp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - temp;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Spell()
    {
        transform.position = Vector3.MoveTowards(transform.position, pos, Speed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damagable")
        {
            try
            {
                collision.gameObject.GetComponent<Enemy_Health>().Damage(Damage);
                Destroy(gameObject);
            }
            catch
            {
                Debug.Log("No, Enemy Health componenet found");
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Spell();
        if(Vector3.Distance(transform.position,pos) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
