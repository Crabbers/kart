using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour 
{
    public GameObject m_playerKart;

	// Use this for initialization
	void Start () 
    {
         
	}

	// Update is called once per frame
	void Update () 
    {
        Vector3 kartPos = m_playerKart.transform.position;
        kartPos.z -= 10.0f;
        this.gameObject.transform.position = kartPos;
    }
}
