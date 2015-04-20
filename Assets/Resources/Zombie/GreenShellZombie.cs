using UnityEngine;
using System.Collections;

public class GreenShellZombie : MonoBehaviour
{
    public float Speed = 2f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * Speed;
    }
}
