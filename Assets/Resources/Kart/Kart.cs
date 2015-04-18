using UnityEngine;
using System.Collections;

public class Kart : MonoBehaviour {

    public WheelCollider m_frontLeft;
    public WheelCollider m_frontRight;
    public WheelCollider m_backLeft;
    public WheelCollider m_backRight;

    private int m_speed = 150;
    private int m_breaking = 1;
    private int m_turning = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(m_backRight.transform.position);
        float verticalAxis = Input.GetAxis("Vertical") * m_speed;
        float horizontalAxis = Input.GetAxis("Horizontal") * m_turning;
        m_backLeft.motorTorque = m_backRight.motorTorque = verticalAxis;
        m_frontLeft.steerAngle = m_frontRight.steerAngle = horizontalAxis;

        if (Input.GetKey(KeyCode.Space)) {
            m_backLeft.brakeTorque = m_backRight.brakeTorque = m_breaking;
        }
	}
}
