using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpawner : MonoBehaviour
{
    public Spawner spawnerScript;
    public GameObject enemy;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(randomNumber());
        if (spawnerScript.canSpawn)
        {
            StartCoroutine(WaitForSpawn());
        }
    }

    IEnumerator randomNumber()
    {
        int randomNumber = Random.Range(1, 4);
        switch(randomNumber)
        {
            case 1:
                enemy = enemy1;
                break;

            case 2:
                enemy = enemy2;
                break;
            case 3:
                enemy = enemy3;
                break;
        }

        yield return new WaitForSeconds(1);

        yield return null;
    }

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(Random.Range(1, 4));

        if (spawnerScript.canSpawn)
        {
            StartCoroutine(Spawn());
        }

        yield return null;
    }

    IEnumerator Spawn()
    {
        GameObject obj = Instantiate(enemy);
        obj.GetComponent<EnemigoMove>().enabled = false;
        obj.transform.position = gameObject.transform.position;
        spawnerScript.Spawns.Add(obj);

        yield return null;
    }
}
