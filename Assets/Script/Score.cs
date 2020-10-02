using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = "Melhor Pontuação: " + GameController.gameController.GetHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
