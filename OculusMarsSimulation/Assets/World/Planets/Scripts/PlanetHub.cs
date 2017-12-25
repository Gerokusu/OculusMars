using UnityEngine;

public class PlanetHub : MonoBehaviour
{
    public Mission[] missionsPrefabs;

    public void Awake()
    {
        foreach(Mission missionPrefab in missionsPrefabs)
        {
            Mission mission = Instantiate(missionPrefab);
            mission.transform.parent = transform;
            mission.transform.localPosition = Navigator.GetMercatorToSphere(mission.locationCoordinates.x, mission.locationCoordinates.y, 0.5F);
            mission.transform.LookAt(transform);
        }
    }
}
