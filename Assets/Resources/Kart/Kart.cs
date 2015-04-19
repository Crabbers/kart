using UnityEngine;
using System.Collections;

public class Kart : MonoBehaviour
{
    public bool Bot = false;

    public Transform FrontRightWheel;
    public Transform FrontLeftWheel;
    public Transform RearRightWheel;
    public Transform RearLeftWheel;

    public float Acceleration = 0.01f;
    public float MaximumForwardVelocity = 1f;
    public float MaximumBackwardVelocity = 1f;
    public float WheelRotationFactor = 100f;

    private float _velocity = 0f;

    private Transform[] _wheels = new Transform[4];

    // Use this for initialization
    void Start()
    {
        _wheels[0] = FrontRightWheel;
        _wheels[1] = FrontLeftWheel;
        _wheels[2] = RearRightWheel;
        _wheels[3] = RearLeftWheel;
    }

    void FixedUpdate()
    {
        _velocity = _velocity + Acceleration * GetMovementMultiplier();

        _velocity = ApplySurfaceFriction(_velocity);

        Mathf.Clamp(_velocity, MaximumBackwardVelocity * -1, MaximumForwardVelocity);

        transform.position = transform.position + transform.forward * _velocity;

        RotateWheels();
    }

    private void RotateWheels()
    {
        foreach(Transform wheel in _wheels)
        {
            wheel.Rotate(wheel.right, _velocity * WheelRotationFactor);
        }
    }

    private int GetMovementMultiplier()
    {
        if(Bot)
        {
            //Bots always drive forward as there is no AI yet
            return 1;
        }
        else
        {
            int movement = 0;

            if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                ++movement;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                --movement;
            }

            return movement;
        }
    }

    private float ApplySurfaceFriction(float VelocityBeforeFriction)
    {
        // This is a temporary measure until we can get the surface friction from the actual road
        float accelerationDueToFriction = 0.001f;

        if(VelocityBeforeFriction < 0f)
        {
            float VelocityAfterFriction = VelocityBeforeFriction + accelerationDueToFriction;
            return Mathf.Clamp(VelocityAfterFriction, VelocityAfterFriction, 0f);
        }
        else if(VelocityBeforeFriction > 0f)
        {
            float VelocityAfterFriction = VelocityBeforeFriction - accelerationDueToFriction;
            return Mathf.Clamp(VelocityAfterFriction, 0f, VelocityAfterFriction);
        }
        else
        {
            return 0f;
        }
    }

}
