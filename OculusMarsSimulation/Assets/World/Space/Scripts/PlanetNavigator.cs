using UnityEngine;

public class PlanetNavigator : Animatable
{
    public PlanetHub planetHub;
    public PlanetMission selectedMission;
    public int selectedMissionIndex;

    private Vector2 positionInitialPlanet = new Vector2(0, 0);
    private Vector3 positionInitialVectorUp = new Vector3(0, 1, 0);

    public void Start()
    {
        SetMission(0, false);
    }

    public void LateUpdate()
    {
        if (planetHub != null && selectedMission != null)
        {
            Vector2 positionPlanet = Vector2.Lerp(positionInitialPlanet, selectedMission.mission.locationCoordinates, animationCurrent);
            transform.localPosition = GetMercatorToSphere(positionPlanet.x, positionPlanet.y, 1.1F);
            transform.LookAt(planetHub.transform, Vector3.Lerp(positionInitialVectorUp, selectedMission.transform.up, animationCurrent));

            if(!isAnimating)
            {
                positionInitial = transform.localPosition;
                positionInitialPlanet = selectedMission.mission.locationCoordinates;
                positionInitialVectorUp = selectedMission.transform.up;
            }
        }
    }

    public void SetMission(int index, bool isAnimated = true)
    {
        if(planetHub != null)
        {
            PlanetMission[] missions = planetHub.GetComponentsInChildren<PlanetMission>();
            int indexRelative = (index + missions.Length) % missions.Length;
            if (missions != null && missions.Length > 0)
            {
                selectedMissionIndex = indexRelative;
                selectedMission = missions[indexRelative];

                if(isAnimated)
                {
                    Animate();
                }
            }
        }
    }
    
    public static Vector3 GetMercatorToSphere(float i, float j, float radius)
    {
        float x = Mathf.Cos(i * Mathf.PI * 2) * Mathf.Sin(j * Mathf.PI) * radius;
        float y = Mathf.Cos(j * Mathf.PI) * radius;
        float z = Mathf.Sin(i * Mathf.PI * 2) * Mathf.Sin(j * Mathf.PI) * radius;
        return new Vector3(x, y, z);
    }
}
