using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool active =false;
    public GameObject PCanvas;

    void Start()
    {
        PCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (active == true)
            {
                Time.timeScale = 1.0f;
                PCanvas.gameObject.SetActive(false);
                active = false;
            }
            else
            {
                Time.timeScale = 0.0f;
                PCanvas.gameObject.SetActive(true);
                active = true;
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        PCanvas.gameObject.SetActive(false);
    }
}    

