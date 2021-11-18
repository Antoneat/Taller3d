using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    public int maxVida;  // Vida m�xima del jugador.
    public int vida;     // Variable de vida donde utilizar� posteriormente para la recuperaci�n de vida despu�s de un tiempo.

    public float tiempo;
    public float maxTiempo; //Tiempo de espera para regenerar la vida.

    public bool godMode; // Modo Dios

    MovPlayer mp;

    private void Start()
    {
        maxVida = vida; // Setea la vida maxima y la vida actual a un mismo valor.
        
    }

    private void Update()
    {
        if (vida < maxVida) //Cuando la vida este igual o mayor a 1 este empezara con un cuenta atras que activara la regeneracion.
        {
            Regeneracion();
        }

        if (Input.GetKeyDown(KeyCode.O)) // Para activar/desactivar el GodMode, pulsa O.
        {
            godMode = !godMode;
        }
    }

    private void Regeneracion() // Cuando termine el tiempo, los puntos de vida comenzaran a subir de 1 en 1;
    {
        tiempo = tiempo + Time.deltaTime;
        if (tiempo >= maxTiempo)
        {
            vida++;
            tiempo = 0;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Moneda"))
        {
            ScoreText.puntaje += 10;  // Incrementar� en 10 el puntaje total cada que se agarre una moneda.
            Destroy(collider.gameObject); // Se autodestruir� la moneda cuando sea "agarrada".
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemigoUno"))
        {
            if (godMode == false || mp.isDashing == false) // Si en godmode esta desactivado, har� lo de abajo.
            {
                vida--;  // Disminuira en 1 la vida cada que choque con un enemigo.

                if (vida <= 0)
                {
                    Destroy(gameObject); // Cuando la vida llegue a 0 el jugador morira.
                }
            }
        }

        if (collision.gameObject.CompareTag("caida"))
        {
            if (godMode == false) // Si en godmode esta desactivado, har� lo de abajo.
            {
                Destroy(gameObject); // Cuando la vida llegue a 0 el jugador morira.
            }
        }

        if (collision.gameObject.CompareTag("obsDesdeA"))
        {
           vida--;
            if (vida <= 0)
            {
                Destroy(gameObject); // Cuando la vida llegue a 0 el jugador morira.
            }
        }

    }
}

