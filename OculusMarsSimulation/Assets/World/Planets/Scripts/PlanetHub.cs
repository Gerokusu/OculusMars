using UnityEngine;

public class PlanetHub : MonoBehaviour
{
    public Mission[] missionsPrefabs;

    public void Start()
    {
        foreach(Mission missionPrefab in missionsPrefabs)
        {
            Mission mission = Instantiate(missionPrefab);
            mission.transform.parent = transform;
            mission.transform.localPosition = GetMercatorToSphere(mission.locationCoordinates.x, mission.locationCoordinates.y);
        }
    }

    public Vector3 GetMercatorToSphere(float i, float j)
    {
        float x = Mathf.Cos(i * Mathf.PI * 2) * Mathf.Sin(j * Mathf.PI) * (0.5F);
        float y = Mathf.Cos(j * Mathf.PI) * (0.5F);
        float z = Mathf.Sin(i * Mathf.PI * 2) * Mathf.Sin(j * Mathf.PI) * (0.5F);
        return new Vector3(x, y, z);
    }
}
