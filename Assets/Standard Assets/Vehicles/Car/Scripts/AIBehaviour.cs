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

    // Use this for initialization
    void Start()
    {
        m_carUserControl = GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>();
        m_isDrone = m_carUserControl.m_isDrone;
        m_agent = GetComponent<NavMeshAgent>();
        m_car = m_carUserControl.gameObject;

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
             NavMeshPath path = new NavMeshPath();
             m_agent.CalculatePath(new Vector3(0, 0, 0), path);
             Vector3 carToNextPos = path.corners[1] - m_car.transform.position;
            bool needAccelarate = carToNextPos.sqrMagnitude > 0.2;
           
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
                    vControl = 0.1f;
                    hControl = quadrant == 2 ? -1 : 1;
                }
            }

            m_carUserControl.DroneControl(hControl, vControl, 0, 0);
        }
    }

    private List<TargetType> AreTargetsAvailable()
    {
        List<TargetType> possibleTargets = new List<TargetType>();

        return possibleTargets;
    }


}
