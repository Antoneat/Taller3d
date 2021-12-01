using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zMovement : MonoBehaviour
{
    public int left;
    public int right;
    public int up;
    public int down;
    public float secs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        secs -= Time.deltaTime * 3;
        if (secs <= 0)
        {
            StartCoroutine(Movement());
            secs = 5;
        }
    }

    IEnumerator Movement()
    {
        transform.position = new Vector3(Random.Range(left, right), Random.Range(up, down), transform.position.z);

        yield return null;
    }
}
