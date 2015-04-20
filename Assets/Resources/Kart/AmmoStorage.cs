using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class AmmoStorage : MonoBehaviour
{
    public int AmmoCount = 0;
    public GameObject BananaPeelZombiePrefab;
    public GameObject GreenShellZombiePrefab;
    public float FireRate = 1;
    public Transform BananaPeelSpawnPoint;
    public Transform GreenShellSpawnPoint;

    private bool firing = false;

    private PlayerType type = PlayerType.Drone;

    void Start()
    {
        CarUserControl car = transform.parent.GetComponent<CarUserControl>();
        if(car)
        {
            type = car.m_playerType;
        }
    }

    void FixedUpdate()
    {
        if (!firing
            && AmmoCount > 0
            && (
                    (type == PlayerType.Player1 && Input.GetButton("P1 Fire1"))
                || (type == PlayerType.Player2 && Input.GetButton("P2 Fire1"))
                )
            )
        {
            StartCoroutine(FireGreenShellZombie());
        }

        if (!firing
            && AmmoCount > 0
            && (
                    (type == PlayerType.Player1 && Input.GetButton("P1 Fire2"))
                || (type == PlayerType.Player2 && Input.GetButton("P2 Fire2"))
                )
            )
        {
            StartCoroutine(FireBananaPeelZombie());
        }
    }

    IEnumerator FireBananaPeelZombie()
    {
        firing = true;
        AmmoCount--;

        Vector3 backward = Vector3.Normalize(new Vector3(transform.forward.x, 0f, transform.forward.z)) * -1;

        Instantiate(
            BananaPeelZombiePrefab,
            new Vector3(BananaPeelSpawnPoint.position.x, 0f, BananaPeelSpawnPoint.position.z),
            Quaternion.LookRotation(backward, Vector3.up));

        yield return new WaitForSeconds(FireRate);
        firing = false;
    }

    IEnumerator FireGreenShellZombie()
    {
        firing = true;
        AmmoCount--;

        Vector3 forward = Vector3.Normalize(new Vector3(transform.forward.x, 0f, transform.forward.z));

        Instantiate(
            GreenShellZombiePrefab,
            new Vector3(GreenShellSpawnPoint.position.x, 0f, GreenShellSpawnPoint.position.z),
            Quaternion.LookRotation(forward, Vector3.up));

        yield return new WaitForSeconds(FireRate);
        firing = false;
    }

    public void AddAmmo()
    {
        AmmoCount++;
    }
}
