using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManeger;
    public int highScore = 0;
    private bool isgameOver;

    // Start is called before the first frame update
    void Start()
    {
        isgameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isgameOver && Input.GetButton("Jump"))
        {
            isgameOver = false;
            SceneManager.LoadScene("Gameplay");
        }
    }
    public bool GetIsgameOver()
    {
        return isgameOver;
    }
    void Awake()
    {

        if (levelManeger == null)
        {
            levelManeger = this;
            DontDestroyOnLoad(this);
        }
        else if (levelManeger != this)
        {
            Destroy(gameObject);
        }
    }


    public void Gameover(int score)
    {
        if (highScore < score)
        {
            highScore = score;
        }
        isgameOver = true;
        SceneManager.LoadScene("Home");

    }

    public int GetHighScore()
    {
        return highScore;
    }
}
