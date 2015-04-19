using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TargetType
{
    None,
    Item,
    Enemy,
    Friendly
}

public class AIBehaviour : MonoBehaviour 
{
    private NavMeshAgent Agent;
    private Vector3 Target;
    private bool HasTarget;
    private bool Searching;
    private bool m_isDrone;

    // Use this for initialization
    void Start()
    {
        UnityStandardAssets.Vehicles.Car.CarUserControl car = GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>();
        m_isDrone = car.m_isDrone;
        Agent = GetComponent<NavMeshAgent>();
        HasTarget = false;
        Searching = true;
        Target = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isDrone)
        {
            if (Searching)
            {
                // TODO: Can see 
            }

            if (HasTarget)
            {
                // TODO: Is target still within range. Is target aquired
            }

            if (!HasTarget)
            {
                // TODO: Implement check if player / NPC / item is in site

                // TODO: Implement wander / character search

            }

            Agent.SetDestination(Target);
            Vector3 pos = Agent.nextPosition;
        }
    }

    private List<TargetType> AreTargetsAvailable()
    {
        List<TargetType> possibleTargets = new List<TargetType>();

        return possibleTargets;
    }
}
