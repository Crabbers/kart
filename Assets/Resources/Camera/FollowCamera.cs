using UnityEngine;
using System.Collections;
using System;

public class FollowCamera : MonoBehaviour
{
    public Transform CameraObject;
    public Transform Position1Reference;
    public Transform Position2Reference;
    public Transform Position3Reference;

    public enum ReferencePosition
    {
        ONE,
        TWO,
        THREE
    }

    public ReferencePosition _referencePosition = ReferencePosition.ONE;

    private Transform _positionReference;

    // Use this for initialization
    void Start()
    {
        MapPositionReference();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(_referencePosition == ReferencePosition.THREE)
            {
                _referencePosition = ReferencePosition.ONE;
            }
            else
            {
                ++_referencePosition;
            }

            MapPositionReference();
        }

        RaycastHit hit;
        Vector3 rayVector = _positionReference.position - transform.position;
        if (Physics.Raycast(transform.position, rayVector.normalized, out hit, rayVector.magnitude))
        {
            CameraObject.position = hit.point;
        }
        else
        {
            CameraObject.position = _positionReference.position;
        }

        CameraObject.LookAt(transform.position);
    }

    private void MapPositionReference()
    {
        switch (_referencePosition)
        {
            case ReferencePosition.ONE:
                _positionReference = Position1Reference;
                break;

            case ReferencePosition.TWO:
                _positionReference = Position2Reference;
                break;

            case ReferencePosition.THREE:
                _positionReference = Position3Reference;
                break;

            default:
                throw new Exception("Wrong");
        }
    }
}
