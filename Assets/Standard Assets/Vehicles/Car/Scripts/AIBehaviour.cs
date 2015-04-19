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
    private float timeStuck = 0.0f;
    private float m_reverseTime = 0.0f;

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
            //Hack to make the agent update its position without trying to move the kart
            m_agent.enabled = false;
            m_agent.transform.position = m_car.transform.position;
            m_agent.transform.rotation = m_car.transform.rotation;
            m_agent.enabled = true;

            if((m_prevPosition - m_car.transform.position).magnitude < 0.0001)
            {
                timeStuck += Time.fixedDeltaTime;
            }
            else
            {
                timeStuck = 0;
            }

            if (timeStuck > 2.0f)
            {
                m_reverseTime = 3.0f;
                timeStuck = 0;
            }

            if(m_reverseTime <= 0)
            {
                DriveToDest(new Vector3(0, 0, 0));
            }
            else
            {
                ReverseUnstuck(m_car.transform.position - (5 * m_car.transform.forward));
            }

            m_prevPosition = m_car.transform.position;
        }
    }

    private void ReverseUnstuck(Vector3 dest)
    {
        m_carUserControl.DroneControl(m_reverseTime > 1.5f ? 1 : -1, -1.0f, -1.0f, 0);
        m_reverseTime -= Time.fixedDeltaTime;
    }

    private void DriveToDest(Vector3 dest)
    {
        
        NavMeshPath path = new NavMeshPath();

        m_agent.CalculatePath(dest, path);
        Vector3 carToNextPos = path.corners[1] - m_car.transform.position;
        bool needAccelarate = (dest - m_car.transform.position).sqrMagnitude > 0.05f;

        Vector3 target = needAccelarate ? carToNextPos.normalized : new Vector3(0, 0, 0);
        Vector3 forward = m_car.transform.forward;
        Vector3 right = m_car.transform.right;
          float fDotT = Vector3.Dot(target, forward);
            float rDotT = Vector3.Dot(target, right);

            int quadrant = 0;

            if(fDotT >= 0)
            {
                if(rDotT >= 0)
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
                 if(rDotT >= 0)
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

            if(needAccelarate)
            {
                const float minAccelaration = 0.2f;
                if(quadrant == 1 || quadrant ==4)
                {
                    vControl = System.Math.Max(minAccelaration, 1.0f * Vector3.Dot(target, forward));
                    if(fDotT < 0.97f)
                    {
                        hControl = quadrant == 1 ? -1 : 1;
                    }
                }
                else
                {
                    vControl = -0.6f;
                    hControl = quadrant == 2 ? -1 : 1;
                }
            }

            m_carUserControl.DroneControl(hControl, vControl, vControl, 0);
    }

    private List<TargetType> AreTargetsAvailable()
    {
        List<TargetType> possibleTargets = new List<TargetType>();

        return possibleTargets;
    }


}
