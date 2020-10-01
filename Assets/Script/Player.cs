using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Objects
    public Animator anim;
    public AudioSource up;
    public ParticleSystem boom;
    private Rigidbody2D rb2d;
    public GameObject bolsa;

    //HUD
    public Text txt_meta;
    public Text txt_life;
    public Text txt_blood;
    public Text txt_lose;
    public Text txt_win;
    private int sangue = 0;
    private int vidas = 3;
    private int meta = 3;
    
    //Moviment
    public int speed;
    public float jumpForce = 350;
    private bool grounded = true;
    public float maxSpeed = 10;
    public float moveForce;
    private float hForce = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetInteger("flow", 0);
        UpHighScore();
        txt_lose.enabled = false;
        txt_win.enabled = false;
}

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
        if (vidas <= 0)
        {
            LevelManager.levelManeger.Gameover(sangue);
        } else if (txt_lose.enabled && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            txt_lose.enabled = false;
        }
        
    }

    private void move()
    {
        float axis = Input.GetAxis("Horizontal") * Time.deltaTime;
        if (axis != 0)
        {
            rb2d.AddForce(Vector2.right * hForce * moveForce * Math.Abs(axis)/axis);
            Debug.Log(rb2d.velocity.x);
            if (Math.Abs(rb2d.velocity.x) > maxSpeed)
            {
                rb2d.velocity = new Vector2(maxSpeed * Math.Abs(axis) / axis, rb2d.velocity.y);
            }
            Vector3 myScale = this.transform.localScale;
            myScale.x = Math.Abs(myScale.x) *Math.Abs(axis)/axis;
            this.transform.localScale = myScale;
        }
        anim.SetFloat("speed", Math.Abs(rb2d.velocity.x));
    }


    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            this.gameObject.tag = "Ataque";
            grounded = false;
            anim.SetInteger("flow", 1);
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
    } 

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            this.gameObject.tag = "Player";
            grounded = true;
            anim.SetInteger("flow", 2);
        }
        else if (collision.gameObject.tag == "Enemy" && this.gameObject.tag == "Player")
        {
            decrementLife();
        }
        else if (collision.gameObject.tag == "Enemy" && this.gameObject.tag == "Ataque")
        {
            Vector2 bolsaDrop = collision.gameObject.transform.position;
            bolsaDrop.x += 2;
            GetComponent<AudioSource>().Play();
            boom.Play();
            Destroy(collision.gameObject);
            Instantiate(bolsa, bolsaDrop, Quaternion.identity);
        }
        else if (collision.gameObject.tag == "Sangue")
        {
            up.Play();
            Destroy(collision.gameObject);
            incrementSangue();
            upSangue();
        } else if (collision.gameObject.tag == "Doc")
        {
            if (sangue >= meta)
            {
                txt_win.enabled = true;
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
            else
            {
                txt_lose.enabled = true;
            }            
            //LevelManager.levelManeger.Gameover(sangue);
        }
    }

    public void upSangue()
    {
        txt_blood.text = "Sangue: " + sangue;
    }
    public void incrementSangue()
    {
        this.sangue += 1;
    }

    private void UpHighScore()
    {
        txt_meta.text = "Meta: " + LevelManager.levelManeger.GetHighScore(); //Meta do nível
    }
    public void decrementLife()
    {
        this.vidas -= 1;
        lessLife();
    }
    private void lessLife()
    {
        txt_life.text = "Vidas: " + vidas;
    }
}
