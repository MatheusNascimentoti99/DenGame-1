using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Blood : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ataque"))
        {
            collision.GetComponent<Player>().up.Play();
            Destroy(gameObject);
            collision.GetComponent<Player>().incrementSangue();
            collision.GetComponent<Player>().upSangue();
        }
    }

}
