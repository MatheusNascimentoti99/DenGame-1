using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameController;
    public int levelMax;

    private int highScore = 0;
    private int levelCorrent = 1;
    private Player player;

    private int scoreCorrent = 0;
    private int lifeCorrent = 3;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getLevelCorrent()
    {
        return levelCorrent;
    }

    public void NextLevel()
    {
        if(levelCorrent < levelMax)
        {
            levelCorrent++;
        }
    }

    private void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
            DontDestroyOnLoad(gameController);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool OverLevelMax()
    {
        return levelMax < levelCorrent;
    }
    public void SetHighScore(int highScore)
    {
        this.highScore = highScore;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public int GetScoreCorrent()
    {
        return scoreCorrent;
    }

    public int GetLifecorrent()
    {
        return lifeCorrent;
    }

    public void SetLifeCorrent(int life)
    {
        this.lifeCorrent = life;
    }

    public void SetScoreCorrent(int score)
    {
        this.scoreCorrent = score;
    }

    public void Reset()
    {
        lifeCorrent = 3;
        scoreCorrent = 0;
    }
}
