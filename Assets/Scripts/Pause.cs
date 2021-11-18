using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool active;
    Canvas canvas;
    

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void Paused()
    {
        active = !active;
        canvas.enabled = active;
        Time.timeScale = (active) ? 0 : 1f;
    }
}
