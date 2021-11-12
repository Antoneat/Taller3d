using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemanaOnce : MonoBehaviour
{

    //  *** TAREA ---> PM-06 ***

    int maxVida;  // Vida m�xima del jugador.
    int vida;     // Variable de vida donde utilizar� posteriormente para la recuperaci�n de vida despu�s de un tiempo.


    //  *** TAREA ---> PM-08 ****

    int puntaje; // Variable que acumular� el total de el valor de las monedas recolectadas. 

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Moneda"))
        {
            Puntaje = Puntaje + 10;  // Incrementar� en 10 el puntaje total cada que se agarre una moneda.
            Destroy(collider.gameObject); // Se autodestruir� la moneda cuando sea "agarrada".
        }
    }
}
