using UnityEngine;

public class PlanetMission : MonoBehaviour
{
    public Mission mission;

    public float time = 0;
    public int timeHour = 0;
    public int timeMinute = 0;
    public int timeSeconds = 0;

    public void Update()
    {
        PlanetTime planetTime = GetComponentInParent<PlanetTime>();
        if(planetTime != null)
        {
            time = planetTime.GetCurrentTime(mission.locationCoordinates.x);
            timeHour = planetTime.GetCurrentHour(time);
            timeMinute = planetTime.GetCurrentMinute(time);
            timeSeconds = planetTime.GetCurrentSecond(time);
        }

        Light light = GetComponentInChildren<Light>();
        if (light != null)
        {
            light.intensity = Mathf.Clamp(Mathf.Sin(time * (Mathf.PI * 2) + Mathf.PI / 2)* 4, 0, 1);
        }
    }
}
