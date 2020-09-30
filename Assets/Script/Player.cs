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
        Vector2 position = this.transform.position;
        Vector3 player = this.transform.localScale;
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            player.x = 4.612264f;
            this.transform.localScale = player;
            anim.SetInteger("flow", 3);
            position.x += Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;          
        } else if(Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            player.x = -4.612264f;
            this.transform.localScale = player;
            anim.SetInteger("flow", 3);
            position.x += Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        } else
        {
            anim.SetInteger("flow", 2);
        }
        this.transform.position = position;
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
