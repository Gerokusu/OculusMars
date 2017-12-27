using UnityEngine;

public class PlanetHub : MonoBehaviour
{
    public PlanetMission planetMissionPrefab;
    public Mission[] missions;

    public void Awake()
    {
        if(planetMissionPrefab != null && missions != null && missions.Length > 0)
        {
            foreach (Mission mission in missions)
            {
                PlanetMission planetMission = Instantiate(planetMissionPrefab);
                planetMission.transform.parent = transform;
                planetMission.transform.localPosition = PlanetNavigator.GetMercatorToSphere(mission.locationCoordinates.x, mission.locationCoordinates.y, 0.5F);
                planetMission.transform.LookAt(transform);
                planetMission.mission = mission;
            }
        }
    }
}
