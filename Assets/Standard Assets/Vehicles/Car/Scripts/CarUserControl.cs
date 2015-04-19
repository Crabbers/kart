using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Linq;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        /// <summary>
        /// true enables drone control
        /// </summary>
        public bool m_isDrone;

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
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
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
