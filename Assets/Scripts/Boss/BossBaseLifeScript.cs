using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseLifeScript : MonoBehaviour
{
    public int life = 3;
    public ScriptMovementBoss leftHand;
    public ScriptMovementBoss rightHand;
    public ScriptMovementBoss head;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life == 3)
        {
            StartCoroutine(LeftHandPhase());
        }
        if (life == 2)
        {
            StartCoroutine(RightHandPhase());
        }
        if (life == 1)
        {
            StartCoroutine(HeadPhase());
        }
        if (life == 0)
        {
            Die();
        }
    }

    IEnumerator LeftHandPhase()
    {
        yield return new WaitForSeconds(2);

        if (leftHand.gameObject)
        {
            leftHand.enabled = true;
        }

        yield return null;
    }

    IEnumerator RightHandPhase()
    {
        yield return new WaitForSeconds(2);

        if (rightHand.gameObject)
        {
            rightHand.enabled = true;
        }

        yield return null;
    }

    IEnumerator HeadPhase()
    {
        yield return new WaitForSeconds(2);

        if (head.gameObject)
        {
            head.enabled = true;
        }

        yield return null;
    }

    public void Die()
    {
        //AQUI SE COLOCA LA TRANSICION A VICTORIA
    }

}
