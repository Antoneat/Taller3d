using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    Text scoreF;

    void Start()
    {
        scoreF = GetComponent<Text>();
    }

    void Update()
    {
        scoreF.text = " " + ScoreText.puntaje;
    }
}
