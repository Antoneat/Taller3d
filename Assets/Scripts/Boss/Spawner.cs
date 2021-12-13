using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Points")]
    public GameObject LeftDown;
    public GameObject LeftMidDown;
    public GameObject MidDown;
    public GameObject RightMidDown;
    public GameObject RightDown;
    public GameObject LeftUp;
    public GameObject LeftMidUp;
    public GameObject MidUp;
    public GameObject RightMidUp;
    public GameObject RightUp;
    [Header("Parts")]
    public GameObject ManoIzquierda;
    public GameObject ManoDerecha;
    public GameObject Cuerpo;

    public float spawnObjetcsNumber;
    public bool canSpawn;

    public List<GameObject> Spawns;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawns.Count > 0)
        {
            if (Spawns[0] == null)
            {
                Spawns.Remove(Spawns[0]);
            }
        }
        if (ManoIzquierda)
        {
            spawnObjetcsNumber = 1;
        }
        else if(ManoDerecha)
        {
            spawnObjetcsNumber = 2;
        }
        else if(Cuerpo)
        {
            spawnObjetcsNumber = 3;
        }

        if(Spawns.Count < spawnObjetcsNumber)
        {
            canSpawn = true;
        }
        else if(Spawns.Count >= spawnObjetcsNumber)
        {
            canSpawn = false;
        }
    }
}
