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
        // _minimapCamera.rect = new Rect(0.5f, -0.6f, 1f, 1f); // View port.
        _minimapCamera.fieldOfView = 40f;
        _minimapCamera.clearFlags = CameraClearFlags.Depth;
        _minimapCamera.transform.rotation = Quaternion.Euler(90, -90, 0);
        _minimapCamera.orthographic = true;

        Vector3 minBounds = maxBounds = Vector3.zero;
        Vector3 = Vector3.zero;
        Vector3 childAverage = Vector3.zero;
        for (int i = 0; i < Map.transform.childCount; ++i)
        {
            Transform mapChild = Map.GetChild(i);
            Renderer childRenderer = mapChild.GetComponent<Renderer>();
            childAverage += mapChild.position;

            maxBounds = new Vector3
            (
                Mathf.Max(maxBounds.x, childRenderer.bounds.size.x),
                Mathf.Max(maxBounds.y, childRenderer.bounds.size.y),
                Mathf.Max(maxBounds.z, childRenderer.bounds.size.z)
            );
            minBounds = new Vector3
            (
                Mathf.Min(minBounds.x, childRenderer.bounds.size.x),
                Mathf.Min(minBounds.y, childRenderer.bounds.size.y),
                Mathf.Min(minBounds.z, childRenderer.bounds.size.z)
            );
        }
        childAverage /= Map.transform.childCount;

        float x = Mathf.Abs(minBounds.x + maxBounds.x);
        float y = Mathf.Abs(minBounds.y + maxBounds.y);
        float z = Mathf.Abs(minBounds.z + maxBounds.z);
        float ortho = 0f;
        if (x >= y)
        {
            if (x >= z)
            {
                ortho = x;
            }
            else
            {
                ortho = z;
            }
        }
        else 
        { // x < y
            if (y >= z)
            {
                ortho = y;
            }
            else
            {
                ortho = z;
            }
        }

        _minimapCamera.orthographicSize = ortho;

        // Set to origin of attached map, but set far above
        // (via a fudged value) for a birds eye view.
        _minimapCamera.transform.localPosition = new Vector3(childAverage.x, 
            childAverage.y * MinimapFudge, childAverage.z);
    }

    void Update()
    {
    }
}
