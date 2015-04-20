using UnityEngine;
using System.Collections;

public class AmmoStorage : MonoBehaviour
{
    public int AmmoCount = 0;
    public int Score = 0;

    public GameObject BananaPeelZombiePrefab;
    public GameObject GreenShellZombiePrefab;
    public float FireRate = 1;
    public Transform BananaPeelSpawnPoint;
    public Transform GreenShellSpawnPoint;

    public int SpinforTicks = 96;
    public float SpinIncrement = 15f;

    private bool firing = false;

    private PlayerType.Types type = PlayerType.Types.Drone;

    private int _spinning = 0;

    void Start()
    {
        PlayerType car = transform.parent.GetComponent<PlayerType>();
        if(car)
        {
            type = car.Player;
        }
    }

    void FixedUpdate()
    {
        if (_spinning > 0)
        {
            --_spinning;
            transform.parent.Rotate(Vector3.up, SpinIncrement);
            return;
        }

        if (!firing
            && AmmoCount > 0
            && (Input.GetMouseButton(0)
                //    (type == PlayerType.Types.Player1 && Input.GetButton("P1 Fire1"))
                //|| (type == PlayerType.Types.Player2 && Input.GetButton("P2 Fire1"))
                )
            )
        {
            StartCoroutine(FireGreenShellZombie());
        }

        if (!firing
            && AmmoCount > 0
            && (Input.GetMouseButton(1)
                //    (type == PlayerType.Types.Player1 && Input.GetButton("P1 Fire2"))
                //|| (type == PlayerType.Types.Player2 && Input.GetButton("P2 Fire2"))
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

        GameObject zombie = Instantiate(
            GreenShellZombiePrefab,
            new Vector3(GreenShellSpawnPoint.position.x, 0f, GreenShellSpawnPoint.position.z),
            Quaternion.LookRotation(forward, Vector3.up)) as GameObject;

        zombie.GetComponent<GreenShellZombie>().Shooter = this;

        yield return new WaitForSeconds(FireRate);
        firing = false;
    }

    public void AddAmmo()
    {
        AmmoCount++;
    }

    public void AddScore()
    {
        AmmoCount++;
    }

    public void DealDamage()
    {
        AmmoCount--;
        _spinning = SpinforTicks;
    }
}
