using UnityEngine;
using System.Collections;

public class ZombieKart : MonoBehaviour
{
    public bool Bot = false;

    public Transform FrontRightWheel;
    public Transform FrontLeftWheel;
    public Transform RearRightWheel;
    public Transform RearLeftWheel;

    public Transform[] WheelModels;

    public float Acceleration = 0.01f;
    public float MaximumForwardVelocity = 1f;
    public float MaximumBackwardVelocity = 1f;
    public float WheelRotationFactor = 100f;
    public float WheelTurnRate = 5f;
    public float MaxTurnAngle = 30f;

    private float _velocity = 0f;
    private float _turnAngle = 0f;

    public float _fuckFactor = 0.15f;

    void FixedUpdate()
    {
        _velocity = _velocity + Acceleration * GetMovementMultiplier();

        _velocity = ApplySurfaceFriction(_velocity);

        _velocity = Mathf.Clamp(_velocity, MaximumBackwardVelocity * -1f, MaximumForwardVelocity);

        transform.position = transform.position + transform.forward * _velocity;

        int turningMultiplier = GetTurningMultiplier();

        _turnAngle += WheelTurnRate * turningMultiplier;

        _turnAngle = Mathf.Clamp(_turnAngle, MaxTurnAngle * -1, MaxTurnAngle);

        if (turningMultiplier == 0)
        {
            ApplyWheelCentring();
        }

        TurnWheel(FrontRightWheel);
        TurnWheel(FrontLeftWheel);

        RotateWheels();

        if (_velocity != 0f)
        {
            float velocityModifier = 1f;
            int forwardOrReverse = _velocity > 0f ? 1 : -1;

            if (forwardOrReverse > 0f)
            {
                velocityModifier = _velocity / MaximumBackwardVelocity;
            }
            else
            {
                velocityModifier = _velocity / MaximumForwardVelocity * -1;
            }

            if(velocityModifier < 0.33f)
            {
                velocityModifier *= velocityModifier;
                velocityModifier *= _fuckFactor * 9;
            }
            else if(velocityModifier > 0.33f)
            {
                velocityModifier = _fuckFactor;
            }

            velocityModifier *= forwardOrReverse;

            Debug.Log("Modifier: " + velocityModifier);

            transform.Rotate(Vector3.down, _turnAngle * velocityModifier);
        }
    }

    private void TurnWheel(Transform wheel)
    {
        wheel.rotation = Quaternion.AngleAxis(_turnAngle * -1, wheel.up) * transform.rotation;
    }

    private void RotateWheels()
    {
        foreach (Transform wheel in WheelModels)
        {
            wheel.Rotate(wheel.right, _velocity * WheelRotationFactor, Space.World);
        }
    }

    private int GetTurningMultiplier()
    {
        if (Bot)
        {
            //Bots always turn to the left
            return 1;
        }
        else
        {
            int turnDirection = 0;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                ++turnDirection;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                --turnDirection;
            }

            return turnDirection;
        }
    }

    private int GetMovementMultiplier()
    {
        if (Bot)
        {
            //Bots always drive forward as there is no AI yet
            return 1;
        }
        else
        {
            int movement = 0;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                ++movement;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                --movement;
            }

            return movement;
        }
    }

    private float ApplySurfaceFriction(float VelocityBeforeFriction)
    {
        // This is a temporary measure until we can get the surface friction from the actual road
        float accelerationDueToFriction = 0.004f;

        if (VelocityBeforeFriction < 0f)
        {
            float VelocityAfterFriction = VelocityBeforeFriction + accelerationDueToFriction;
            return Mathf.Clamp(VelocityAfterFriction, VelocityAfterFriction, 0f);
        }
        else if (VelocityBeforeFriction > 0f)
        {
            float VelocityAfterFriction = VelocityBeforeFriction - accelerationDueToFriction;
            return Mathf.Clamp(VelocityAfterFriction, 0f, VelocityAfterFriction);
        }
        else
        {
            return 0f;
        }
    }

    private void ApplyWheelCentring()
    {
        if (_turnAngle < 0f)
        {
            _turnAngle += WheelTurnRate;
            _turnAngle = Mathf.Clamp(_turnAngle, _turnAngle, 0f);
        }
        else if (_turnAngle > 0f)
        {
            _turnAngle -= WheelTurnRate;
            _turnAngle = Mathf.Clamp(_turnAngle, 0f, _turnAngle);
        }
    }

}
