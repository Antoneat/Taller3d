using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    public int maxVida;  // Vida máxima del jugador.
    public int vida;     // Variable de vida donde utilizará posteriormente para la recuperación de vida después de un tiempo.

    public float tiempo;
    public float maxTiempo; //Tiempo de espera para regenerar la vida.

    public bool godMode; // Modo Dios

    MovPlayer mp;

    public GameObject MonedaParticula;
    public GameObject particulaMoneda;

    public GameObject RegenDeVida;

    public GameObject RecibiendoDmg;
    public GameObject particulaDmg;

    public Image BarraDeScore;

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
        if(vida==maxVida)
        {
            RegenDeVida.SetActive(false);
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
            RegenDeVida.SetActive(true);
            tiempo = 0;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Moneda"))
        {
            ScoreText.puntaje += 10;  // Incrementará en 10 el puntaje total cada que se agarre una moneda.
            BarraDeScore.fillAmount += (ScoreText.puntaje/1000); 
            Destroy(collider.gameObject); // Se autodestruirá la moneda cuando sea "agarrada".
            MonedaParticula = Instantiate(particulaMoneda, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemigoUno"))
        {
            if (godMode == false || mp.isDashing == false) // Si en godmode esta desactivado, hará lo de abajo.
            {
                vida--;  // Disminuira en 1 la vida cada que choque con un enemigo.
                RecibiendoDmg = Instantiate(particulaDmg, transform.position, Quaternion.identity);

                if (vida <= 0)
                {
                    Destroy(gameObject); // Cuando la vida llegue a 0 el jugador morira.
                }
            }
        }

        if (collision.gameObject.CompareTag("caida"))
        {
            if (godMode == false) // Si en godmode esta desactivado, hará lo de abajo.
            {
                Destroy(gameObject); // Cuando la vida llegue a 0 el jugador morira.
            }
        }

        if (collision.gameObject.CompareTag("obsDesdeA"))
        {
            vida--;
            RecibiendoDmg = Instantiate(particulaDmg, transform.position, Quaternion.identity);

            if (vida <= 0)
            {
                Destroy(gameObject); // Cuando la vida llegue a 0 el jugador morira.
            }
        }

        if (collision.gameObject.tag == "pasarLVL1")
        {
            SceneManager.LoadScene("Nivel2");
        }

        if (collision.gameObject.tag == "pasarLVL2")
        {
            SceneManager.LoadScene("Nivel3");
        }

        if (collision.gameObject.tag == "pasarLVL3")
        {
            SceneManager.LoadScene("Nivel4");
        }
    }
}

