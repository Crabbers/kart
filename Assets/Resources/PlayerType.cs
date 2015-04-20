using UnityEngine;
using System.Collections;

public class PlayerType : MonoBehaviour
{
    public enum Types
    {
        Drone,
        Player1,
        Player2
    };

    public Types Player;
}
