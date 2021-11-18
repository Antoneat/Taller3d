using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
	//public float awareAI = 20f;
	public float Speed;
	//public float damping = 6.0f;

	public Transform[] navPoint;
	public UnityEngine.AI.NavMeshAgent agent;
	public int destPoint = 0;
	public Transform goal;


	void Start()
	{
		UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position;

		agent.autoBraking = false;

	}

	void Update()
	{

		if (agent.remainingDistance < 1f)
			GotoNextPoint();

	}


	public void GotoNextPoint()
	{
		
		if (navPoint.Length <= 0)
			return;
		agent.destination = navPoint[destPoint].position;
		destPoint = (destPoint + 1) % navPoint.Length;
		
	}

}
