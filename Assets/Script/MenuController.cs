using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 

    public static void LoadPlay()
    {
        Debug.Log("Level" + GameController.gameController.getLevelCorrent().ToString());  
        SceneManager.LoadScene("Level"+GameController.gameController.getLevelCorrent().ToString());
    }

    public static void LoadHome()
    {
        SceneManager.LoadScene("Home");
    }

    public static void LoadAbout()
    {
        SceneManager.LoadScene("About");
    }

    public static void Exit()
    {
        Application.Quit(0);
        //EditorApplication.isPlaying = false;
    }

}
