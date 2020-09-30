using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int speed;
    public Animator anim;
    public float jumpForce = 700;
    private Rigidbody2D rb2d;
    private bool grounded = true;
    public float maxSpeed = 10;
    public GameObject bolsa;
    public Text txt_highScore;
    public Text txt_life;
    public Text txt_blood;
    private int sangue = 0;
    private int vidas = 3;
    public AudioSource up;

    public float moveForce;
    private float hForce = 1;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetInteger("flow", 0);
        UpHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
        if (vidas <= 0)
        {
            LevelManager.levelManeger.Gameover(sangue);
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            grounded = false;
            anim.SetInteger("flow", 1);
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
    } 

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            anim.SetInteger("flow", 2);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Vector2 bolsaDrop = collision.gameObject.transform.position;
            bolsaDrop.x += 2;
            GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
            Instantiate(bolsa, bolsaDrop, Quaternion.identity);

        }
        else if (collision.gameObject.tag == "Sangue")
        {
            up.Play();
            Destroy(collision.gameObject);
            incrementSangue();
            upSangue();
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
        txt_highScore.text = "HighScore: " + LevelManager.levelManeger.GetHighScore();
    }
    public void decrementLife()
    {
        this.vidas -= 1;
    }

}
