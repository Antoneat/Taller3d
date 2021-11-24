using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    public float timeToDie;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDie);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
