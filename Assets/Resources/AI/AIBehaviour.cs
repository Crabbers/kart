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
        WayPoints.Add(new Vector3(-3.8f, 0.2f, -63.3f));
        WayPoints.Add(new Vector3(-30f, 0.2f, 17f));
        WayPoints.Add(new Vector3(18f, 0.2f, 65f));
        WayPoints.Add(new Vector3(22f, 0.2f, -1f));
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
