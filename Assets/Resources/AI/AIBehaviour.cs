using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBehaviour : MonoBehaviour 
{
    private NavMeshAgent Agent;
    private List<Vector3> WayPoints;
    private int Target;

	// Use this for initialization
	void Start ()
    {
        Agent = GetComponent<NavMeshAgent>();
        WayPoints = new List<Vector3>();
        Target = 0;

        GameObject track = GameObject.FindGameObjectWithTag("Track");
        Transform[] trackChildren = track.GetComponentsInChildren<Transform>();

        foreach (Transform child in trackChildren)
        {
            if(child.gameObject.tag == "WayPoint")
            {
                WayPoints.Add(child.transform.position);
            }
        }

        Agent.SetDestination(WayPoints[Target]);
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (Vector3.Distance(Agent.transform.position, WayPoints[Target]) <= 20.0f)
        {
            Target++;

            if (Target > WayPoints.Count-1)
            {
                Target = 0;
            }
                
            Agent.SetDestination(WayPoints[Target]);
        }
	}
}
