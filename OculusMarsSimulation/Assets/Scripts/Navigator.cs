using UnityEngine;

public class Navigator : Animatable
{
    public Transform planet;
    public Mission selectedMission;
    public Vector2 positionInitialPlanet = new Vector2(0, 0);
    private Vector3 positionInitialVectorUp = new Vector3(0, 1, 0);

    public bool isOnPlanet = false;

    public override void OnUpdate()
    {
        if (planet != null && selectedMission != null)
        {
            Vector3 vectorUp = Vector3.Lerp(positionInitialVectorUp, selectedMission.transform.up, animationCurrent);
            transform.LookAt(planet, vectorUp);

            if(!isAnimating)
            {
                positionInitialVectorUp = selectedMission.transform.up;
            }
        }
    }

    public override void OnAnimationWork()
    {
        if(selectedMission != null)
        {
            animationCurrent = isOnPlanet ? animationCurrent : 1;

            Vector2 positionPlanet = Vector2.Lerp(positionInitialPlanet, selectedMission.locationCoordinates, animationCurrent);
            transform.localPosition = GetMercatorToSphere(positionPlanet.x, positionPlanet.y, 0.75F);

            isOnPlanet = true;
        }
    }

    public override void OnAnimationEnd()
    {
        positionInitialPlanet = selectedMission.locationCoordinates;
    }
    
    public static Vector3 GetMercatorToSphere(float i, float j, float radius)
    {
        float x = Mathf.Cos(i * Mathf.PI * 2) * Mathf.Sin(j * Mathf.PI) * radius;
        float y = Mathf.Cos(j * Mathf.PI) * radius;
        float z = Mathf.Sin(i * Mathf.PI * 2) * Mathf.Sin(j * Mathf.PI) * radius;
        return new Vector3(x, y, z);
    }
}
