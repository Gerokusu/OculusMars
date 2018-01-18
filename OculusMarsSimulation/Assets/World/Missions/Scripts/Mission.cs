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

    public int locationScene;
}
