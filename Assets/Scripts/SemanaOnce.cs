using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemanaOnce : MonoBehaviour
{

    //  *** TAREA ---> PM-06 ***

    int maxVida;  // Vida máxima del jugador.
    int vida;     // Variable de vida donde utilizará posteriormente para la recuperación de vida después de un tiempo.


    //  *** TAREA ---> PM-08 ****

    int puntaje; // Variable que acumulará el total de el valor de las monedas recolectadas. 

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Moneda"))
        {
            Puntaje = Puntaje + 10;  // Incrementará en 10 el puntaje total cada que se agarre una moneda.
            Destroy(collider.gameObject); // Se autodestruirá la moneda cuando sea "agarrada".
        }
    }
}
