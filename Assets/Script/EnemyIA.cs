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
    private int control = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move();
    }

    private void move() {

        Vector2 position = this.transform.position;

        if (control==0) {
            for (int i = 1; i <= 5; i++)
            {
                faceRight();
                position.x += i * speed * Time.deltaTime;
                this.transform.position = position;
            }
            control++;
        } else if (control==1) {
            for (int i = 1; i <= 5; i++)
            {
                faceLeft();
                position.x -= i * speed * Time.deltaTime;
                this.transform.position = position;
            }
            control++;
        } else
        {
            control = 0;
        }

    }

    private void faceRight()
    {
        Vector3 mosquito = this.transform.localScale;
        mosquito.x = -0.45104f;
        this.transform.localScale = mosquito;
    }

    private void faceLeft()
    {
        Vector3 mosquito = this.transform.localScale;
        mosquito.x = 0.45104f;
        this.transform.localScale = mosquito;
    }


}
