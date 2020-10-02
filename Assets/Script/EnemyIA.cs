using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{

    public int speed;
    public Rigidbody2D rb2d;
    private int directions;

    // Start is called before the first frame update
    void Start()
    {
        faceRight();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move() {
        rb2d.velocity = new Vector2(speed * directions, rb2d.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (directions == 1)
        {
            faceLeft();
        }
        else
        {
            faceRight();
        }
    }

    private void faceRight()
    {
        directions = 1;
        Vector3 mosquito = this.transform.localScale;
        mosquito.x = -0.45104f;
        this.transform.localScale = mosquito;
    }

    private void faceLeft()
    {
        directions = -1;
        Vector3 mosquito = this.transform.localScale;
        mosquito.x = 0.45104f;
        this.transform.localScale = mosquito;
    }
}
