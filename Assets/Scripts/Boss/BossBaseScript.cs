using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseScript : MonoBehaviour
{
    public float range;
    public float speed;
    public float minSpeed;
    public float maxSpeed;
    public int secondsToWait;
    public Rigidbody rb;
    [SerializeField]
    private bool isPlayerClose;
    LayerMask playerLayer;

    public GameObject DerrotaLvl;
    // Start is called before the first frame update
    void Start()
    {
        speed = minSpeed;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.transform.position += rb.transform.forward * speed * Time.deltaTime;
    }

    void Update()
    {
        if(isPlayerClose)
        {
            StartCoroutine(runThePlayer(secondsToWait));
        }
    }

    IEnumerator runThePlayer(int sec)
    {
        speed = maxSpeed;

        yield return new WaitForSeconds(sec);

        speed = minSpeed;

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pasarLVL4")
        {
            DerrotaLvl.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerClose = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }
}
