using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public static float puntaje = 0; // Variable que acumulará el total de el valor de las monedas recolectadas. 
    Text score;

    void Start()
    {
        score = GetComponent<Text>();
    }

    void Update()
    {
        score.text = "X " + puntaje;
    }
}
