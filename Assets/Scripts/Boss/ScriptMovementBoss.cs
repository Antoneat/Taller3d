using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMovementBoss : MonoBehaviour
{
    public BossBaseLifeScript bossMain;
    public MovPlayer Player;
    public Rigidbody rb;
    public GameObject zPlaceToMove;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public Vector3 randomVector;
    public float speed;
    public life lifeBoss;
    public life readyToStart;
    public bool isMyTurn;
    public Animator anim;
    public enum life
    {
        One,
        Two,
        Three,
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bossMain.life == 3)
        {
            lifeBoss = life.Three;
        }
        if (bossMain.life == 2)
        {
            lifeBoss = life.Two;
        }
        if (bossMain.life == 1)
        {
            lifeBoss = life.One;
        }

        if(readyToStart == lifeBoss)
        {
            isMyTurn = true;
        }

        if (isMyTurn)
        {
            MovementAndInstantiate();
        }
    }

    public void MovementAndInstantiate()
    {
        if(randomVector == Vector3.zero)
        {
            randomVector = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), zPlaceToMove.transform.position.z);
            Debug.Log("Asignado");
            return;
        }

        rb.transform.position = Vector3.MoveTowards(transform.position, zPlaceToMove.transform.position, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, randomVector) < 3)
        {
            randomVector = Vector3.zero;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("a");
            if(Player.dashCollision)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        if (lifeBoss == life.One && isMyTurn)
        {
            if(anim)
            {
                anim.SetTrigger("TimeToDie");
                StartCoroutine(TimeToDie());
            }
        }
        else
        {
            bossMain.life--;
            gameObject.SetActive(false);
        }
    }

    public IEnumerator TimeToDie()
    {

        yield return new WaitForSeconds(1.5f);

        bossMain.life--;
        gameObject.SetActive(false);

        yield return null;
    }
}
