using UnityEngine;
using System.Collections;

/**
 * Define a Mini-Map for our Kart thing.
 * Based on: http://answers.unity3d.com/questions/20676/how-do-i-create-a-minimap.html.
 */
public class Minimap : MonoBehaviour
{
    // transform.position y *= MimimapFudge.
    public float MinimapFudge = 6.0f;

    // Set mini-map relative to the map/level.
    public Transform Map;

    // public Camera PlayerCamera; // Ensure depths are set correctly.
    private Camera _minimapCamera;

    void Start()
    {
        transform.parent = Map; // Relative to map.
        _minimapCamera = GetComponent<Camera>(); // Retrieve attached camera component.

        Vector3 childAverage = Vector3.zero;
        for (int child = 0; child < Map.transform.childCount; ++child)
        {
            childAverage += Map.GetChild(child).transform.position;
        }
        childAverage /= Map.transform.childCount;

        // Set to origin of attached map, but set far above
        // (via a fudged value) for a birds eye view.
        _minimapCamera.transform.localPosition = new Vector3(childAverage.x, 
            childAverage.y * MinimapFudge, childAverage.z);

        _minimapCamera.rect = new Rect(0.5f, -0.6f, 1f, 1f); // View port.
        _minimapCamera.fieldOfView = 40f;
        _minimapCamera.clearFlags = CameraClearFlags.Color;
        _minimapCamera.transform.rotation = Quaternion.Euler(90, -90, 0);
    }
}
