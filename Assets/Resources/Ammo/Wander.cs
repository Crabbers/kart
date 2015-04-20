using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour
{
    public NavMeshAgent Agent;
    public float WalkRadius = 50f;
    public float TargetRadius = 2f;

    // Use this for initialization
    void Start()
    {
        Agent.SetDestination(GetNextDestination());
    }

    // Update is called once per frame
    void Update()
    {
        float squareDistanceToDestination = (Agent.destination - transform.position).magnitude;

        if (squareDistanceToDestination < TargetRadius)
        {
            Agent.destination = GetNextDestination();
        }
    }

    Vector3 GetNextDestination()
    {
        Vector3 randomOffset = Random.insideUnitSphere * WalkRadius;

        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + randomOffset, out hit, WalkRadius, 1);
        return hit.position;
    }
}
