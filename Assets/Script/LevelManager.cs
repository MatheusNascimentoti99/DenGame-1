
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManeger;
    public GameObject txt_noFinish;
    public GameObject txt_gameOver;
    public GameObject txt_win;
    private int highScore = 0;
    private bool isPause;
    public int meta;
    public Text txt_infoLevel;


    // Start is called before the first frame update
    void Start()
    {
        meta = GameController.gameController.GetScoreCorrent() + meta;
        highScore = GameController.gameController.GetHighScore();
        UpHighScore();
        txt_noFinish.SetActive(false);
        txt_win.SetActive(false);
        txt_gameOver.SetActive(false);
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (txt_gameOver.activeSelf && Input.GetButton("Jump"))
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            MenuController.LoadHome();
        }
        if (txt_win.activeSelf && Input.GetButton("Jump"))
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            if (GameController.gameController.OverLevelMax())
            {
                GameController.gameController.ResetGame();
                txt_win.GetComponentsInChildren<Text>()[0].text = "Você concluiu todas as Fases";
                MenuController.LoadHome();
            }
            else
            {
                MenuController.LoadPlay();

            }
        }
    }

    private void Awake()
    {
        if(levelManeger == null)
        {
            levelManeger = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CheckGameover(int bloodGot, int life)
    {
        if (life <= 0)
        {
            GameController.gameController.ResetGame();
            txt_gameOver.SetActive(true);
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        if (highScore < bloodGot)
        {
            GameController.gameController.SetHighScore(bloodGot);
            highScore = bloodGot;
        }

    }

    public void CheckReturnNoFinish()
    {
         if (txt_noFinish.activeSelf && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            txt_noFinish.SetActive(false);
        }
    }

    public void CheckMeta(int bloodGot, int life)
    {
        if (bloodGot >= meta)
        {
            txt_win.SetActive(true);
            GameController.gameController.SetScoreCorrent(bloodGot);
            GameController.gameController.SetLifeCorrent(life);
            GameController.gameController.NextLevel();
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        else if(bloodGot < meta)
        {
            txt_noFinish.SetActive(true);
        }
    }

    public void CheckPause()
    {
        if (Input.GetKey(KeyCode.V))
        {
            isPause = !isPause;
            if (isPause)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
            else
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
            }
        }
    }
    private void UpHighScore()
    {
        txt_infoLevel.text = "Meta: " + LevelManager.levelManeger.GetMeta()  + "    Level:" + GameController.gameController.getLevelCorrent(); //Meta do nível
    }

    private int GetMeta()
    {
        return meta;
    }


    public int GetHighScore()
    {
        return highScore;
    }
}
