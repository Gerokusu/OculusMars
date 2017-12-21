using UnityEngine;

public class Mission : MonoBehaviour
{
    public const string DEFAULT_DESCRIPTION = "###";
    public const string DEFAULT_LOCATION_NAME = "Unkown location";

    public uint id = 1;
    [Multiline]
    public string description = DEFAULT_DESCRIPTION;
    [Range(1, 3)]
    public uint difficulty = 1;

    public string locationName = DEFAULT_LOCATION_NAME;
    public Vector2 locationCoordinates;

    public void Start()
    {
        //Vector3 position = GetSphereToLocal(new Vector2(0, 0), 40);
        //Instantiate(test, position, Quaternion.Euler(Vector3.zero));
	}

    /*
    public Vector2 GetMercatorToSphere(float x, float y, float radius = 1)
    {
        float longitude = (radius > 0) ? x / radius : 0;
        float latitude = (radius > 0) ? 2 * Mathf.Atan(Mathf.Exp(y / radius)) - Mathf.PI / 2 : 0;
        return new Vector2(longitude, latitude);
    }

    public Vector3 GetSphereToLocal(Vector2 coordinates, float radius)
    {
        float x = radius * Mathf.Cos(coordinates.y) * Mathf.Cos(coordinates.x);
        float y = radius * Mathf.Cos(coordinates.y) * Mathf.Sin(coordinates.x);
        float z = radius * Mathf.Sin(coordinates.y);
        return new Vector3(x, y, z);
    }
    */
}
