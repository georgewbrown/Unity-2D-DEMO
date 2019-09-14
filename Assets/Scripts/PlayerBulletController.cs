﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    Rigidbody2D rb2d;

    [SerializeField]
    private float bulletSpeed = 3f;

    [SerializeField]
    private float bulletHeight = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("DestroySelf", .5f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(bulletSpeed, bulletHeight);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            {
                DestroySelf();
            }
        }   
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
