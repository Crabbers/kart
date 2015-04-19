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
    private NavMeshAgent m_agent;
    private bool m_isDrone;
    private GameObject m_car;
    UnityStandardAssets.Vehicles.Car.CarUserControl m_carUserControl;
    private Vector3 m_prevPosition;
    private bool m_moving;
    private float m_timeStuck = 0.0f;
    private float m_reverseTime = 0.0f;
    private Vector3 m_startReversePosition = new Vector3(0, 0, 0);
    private bool shouldReverse = false;
    private int m_lastSteeringSign = 0;

    // Use this for initialization
    void Start()
    {
        m_carUserControl = GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>();
        m_isDrone = m_carUserControl.m_isDrone;
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.updatePosition = false;
        m_agent.updateRotation = false;
        m_car = m_carUserControl.gameObject;
        m_prevPosition = m_car.transform.position;

        if(!m_isDrone)
        {
            m_agent.enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_isDrone)
        {
            Vector3 destination = new Vector3(-10, 0, 20); //this is now only for testing...
            bool stopAtTarget = true;

            //make these public members if require tuning
            const float timeStuckBeforeReverse = 2.0f;
            const float maxReverseTime = 3.0f;
            const float movingThreshold = 0.3f; //0.3ms

            Vector3 carPosition = m_car.transform.position;

            UpdateNavMapAgentTransform();

            //considered moving if more than 0.3m/s
            bool isMoving = (m_prevPosition - carPosition).magnitude / Time.fixedDeltaTime > movingThreshold;

            if(!isMoving)
            {
                m_timeStuck += Time.fixedDeltaTime;
            }
            else
            {
                m_timeStuck = 0.0f;
            }

            if (m_timeStuck > timeStuckBeforeReverse)
            {
                // if not already reversing
                if (m_reverseTime <= 0)
                {
                    m_reverseTime = maxReverseTime;
                    m_startReversePosition = carPosition;
                    shouldReverse = true;
                }
            }

            if (!shouldReverse)
            {
                DriveToDest(destination, stopAtTarget);
            }
            else
            {
                const float sqrDistToReverse = 7.0f; //sqr only for performance considerations
                ReverseUnstuck(m_car.transform.position - (sqrDistToReverse * m_car.transform.forward));
                m_reverseTime -= Time.fixedDeltaTime;
                //reversed three meters or reversed for whole duration
                if (m_reverseTime <= 0 || (carPosition - m_startReversePosition).sqrMagnitude > sqrDistToReverse)
                {
                    shouldReverse = false;
                    m_reverseTime = 0.0f;
                    m_timeStuck = 0.0f;
                }
            }

            m_prevPosition = carPosition;
        }
    }

    private void UpdateNavMapAgentTransform()
    {
        //Hack to make the agent update its position without trying to move the kart
        m_agent.enabled = false;
        m_agent.transform.position = m_car.transform.position;
        m_agent.transform.rotation = m_car.transform.rotation;
        m_agent.enabled = true;
    }

    private void ReverseUnstuck(Vector3 dest)
    {
        m_carUserControl.DroneControl(m_lastSteeringSign < 0 ? 1 : -1, -1.0f, -1.0f, 0);
    }

    private void DriveToDest(Vector3 dest, bool stopAtTarget)
    {
        Vector3 carPosition = m_car.transform.position;
        NavMeshPath path = new NavMeshPath();

        m_agent.CalculatePath(dest, path);
        Vector3 carToNextPos = path.corners[1] - carPosition;
        bool reachedDest = (dest - carPosition).sqrMagnitude <= 0.3f;

        if (!reachedDest)
        {
            Vector3 target = carToNextPos.normalized;
            Vector3 forward = m_car.transform.forward;
            Vector3 right = m_car.transform.right;
            float fDotT = Vector3.Dot(target, forward);
            float rDotT = Vector3.Dot(target, right);

            // quadrants from left to forward vector of car in anti clockwise order
            int quadrant = 0;

            if (fDotT >= 0)
            {
                if (rDotT >= 0)
                {
                    quadrant = 4;
                }
                else
                {
                    quadrant = 1;
                }
            }
            else
            {
                if (rDotT >= 0)
                {
                    quadrant = 3;
                }
                else
                {
                    quadrant = 2;
                }
            }

            float hControl = 0.0f;
            float vControl = 0.0f;
            float brake = 0.0f;

            const float minAccelaration = 0.2f;
            if (quadrant == 1 || quadrant == 4)
            {
                vControl = System.Math.Max(minAccelaration, System.Math.Abs(Vector3.Dot(target, forward)));

                if (fDotT < 0.97f)
                {
                    const float magicSteeringScaler = 3.0f;
                    hControl = magicSteeringScaler * System.Math.Abs(fDotT);
                    hControl = System.Math.Min(hControl, 1.0f);
                    hControl = quadrant == 1 ? -hControl : hControl;
                }
            }
            else
            {
                vControl = 0.0f;
                hControl = quadrant == 2 ? -1 : 1;
                brake = 0.4f;
            }

            //if close to dest try to control speed
            if (stopAtTarget && (carPosition - dest).magnitude < 5.0f)
            {
                float speed = (m_prevPosition - carPosition).magnitude / Time.fixedDeltaTime;
                if (speed > 5)
                {
                    vControl = 0.0f;
                    brake = speed * 0.3f + 0.5f;
                }
            }
            m_lastSteeringSign = System.Math.Sign(hControl);

            m_carUserControl.DroneControl(hControl, vControl, brake, 0);
        }
    }

    private List<TargetType> AreTargetsAvailable()
    {
        List<TargetType> possibleTargets = new List<TargetType>();

        return possibleTargets;
    }


}
