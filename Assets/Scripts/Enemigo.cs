using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vida;


    void Start()
    {
        vida = 1;
    }

    void Update()
    {
        Muerte();
    }

    private void Muerte()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
