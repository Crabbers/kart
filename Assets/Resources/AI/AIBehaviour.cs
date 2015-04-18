using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBehaviour : MonoBehaviour 
{
    NavMeshAgent agent;
    private List<Vector3> WayPoints;
    private int Target;

	// Use this for initialization
	void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
        WayPoints = new List<Vector3>();
        WayPoints.Add(new Vector3(-11f, 0.2f, 3.5f));
        WayPoints.Add(new Vector3(0.5f, 0.2f, 10f));
        WayPoints.Add(new Vector3(9.5f, 0.2f, 3.5f));
        WayPoints.Add(new Vector3(0.5f, 0.2f, -1.5f));
        Target = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        agent.SetDestination(WayPoints[Target]);

        if (Vector3.Distance(agent.transform.position, WayPoints[Target]) <= 5.0f)
        {
            Target++;

            if(Target > 3)
            {
                Target = 0;
            }
        }
	}
}
