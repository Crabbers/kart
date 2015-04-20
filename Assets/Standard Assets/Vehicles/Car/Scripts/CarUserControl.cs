using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Linq;

namespace UnityStandardAssets.Vehicles.Car
{
    public enum PlayerType
    {
        Drone,
        Player1,
        Player2
    };

    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        [HideInInspector]
        public bool m_isDrone
        {
            get
            { 
                return m_playerType == PlayerType.Drone;
            }
        }
        public PlayerType m_playerType;

        private CarController m_Car; // the car controller we want to use


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
           if(m_isDrone)
           {

           }
           else
           {
               UserControl();
           }
        }

        private void UserControl()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis(m_playerType == PlayerType.Player1 ? "P1 Steer" : "P2 Steer");
            float v = CrossPlatformInputManager.GetAxis(m_playerType == PlayerType.Player1 ? "P1 Accelerate" : "P2 Accelerate");
            m_Car.Move(h, v, v, 0f);
        }

        public void DroneControl(float steering, float accel, float footbrake, float handbrake)
        {
            m_Car.Move(steering, accel, footbrake, handbrake);
        }

        private GameObject FindClosestEnemy()
        {
            GameObject[] cars = GameObject.FindGameObjectsWithTag("Drone");
            cars.Concat(GameObject.FindGameObjectsWithTag("Player"));
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in cars)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            return closest;
        }
    }
}
