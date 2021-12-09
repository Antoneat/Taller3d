using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vida;
    public MovPlayer mp;

    void Start()
    {
        vida = 1;

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        mp= Player.GetComponent<MovPlayer>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(mp.currentDashing == 0)
                vida--;  // Disminuira en 1 la vida cada que choque con un enemigo.
                
                if (vida <= 0)
                {
                    Destroy(gameObject); // Cuando la vida llegue a 0 el jugador morira.
                }
            
        }
    }

}
