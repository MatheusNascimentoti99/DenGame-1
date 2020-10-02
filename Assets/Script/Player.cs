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
    public Text txt_life;
    public Text txt_blood;
    private int sangue;
    private int vidas;

    //Moviment
    public int hForceShot;
    public int speed;
    public float jumpForce = 350;
    private bool grounded = true;
    public float maxSpeed = 10;
    public float moveForce;
    private float hForce = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        anim.SetInteger("flow", 0);
        vidas = GameController.gameController.GetLifecorrent();
        sangue = GameController.gameController.GetScoreCorrent();
        upSangue();
        lessLife();


    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
        LevelManager.levelManeger.CheckGameover(sangue, vidas);
        LevelManager.levelManeger.CheckReturnNoFinish();
        LevelManager.levelManeger.CheckPause();


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
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.gameObject.tag = "Player";
            grounded = true;
            anim.SetInteger("flow", 2);
        }
        else if (collision.gameObject.CompareTag("Enemy") && this.gameObject.CompareTag("Player"))
        {

            rb2d.velocity = new Vector2(0,0);
            rb2d.AddForce(new Vector2(-rb2d.centerOfMass.x/Math.Abs(rb2d.centerOfMass.x)*3, 3), ForceMode2D.Impulse);
            decrementLife();
        }
        else if (collision.gameObject.CompareTag("Enemy") && this.gameObject.CompareTag("Ataque"))
        {
            Vector2 bolsaDrop = collision.gameObject.transform.position;
            bolsaDrop.x += 2;
            GetComponent<AudioSource>().Play();
            boom.Play();
            Destroy(collision.gameObject);
            Instantiate(bolsa, bolsaDrop, Quaternion.identity);
        }
         else if (collision.gameObject.CompareTag("Doc"))
        {
            LevelManager.levelManeger.CheckMeta(sangue, vidas);
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
