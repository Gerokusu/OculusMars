using UnityEngine;

public class PlanetTime : MonoBehaviour
{
    public const uint DEFAULT_INT_SECONDS_IN_HOURS = 3600;
    public const uint DEFAULT_INT_SECONDS_IN_MINUTES = 60;
    public const uint DEFAULT_INT_MINUTES_IN_HOURS = 60;
    
    public uint dayHours = 24;
    public uint dayMinutes = 0;
    public uint daySeconds = 0;

    public float time;
    private ulong timeMax;

    public float rotationSpeed = 1;
    public Vector3 rotationAxis = new Vector3(0, 1, 0);
    private Vector3 rotationInitial;

    public Material skyboxMaterial;
    public float skyboxExposureStart;
    public float skyboxExposureEnd;

    public void Start()
    {
        timeMax = dayHours * DEFAULT_INT_SECONDS_IN_HOURS + dayMinutes * DEFAULT_INT_SECONDS_IN_MINUTES + daySeconds;
        rotationInitial = transform.rotation.eulerAngles;
    }

    public void Update()
    {
        time = (time + Time.deltaTime * rotationSpeed) % (uint)timeMax;
        
        float timeScaled = time / timeMax;
        transform.rotation = Quaternion.Euler(rotationInitial);
        transform.Rotate(rotationAxis, timeScaled * 360, Space.World);

        if (skyboxMaterial != null)
        {
            skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(skyboxExposureStart, skyboxExposureEnd, Mathf.Sin(timeScaled * Mathf.PI)));
        }
    }

    public float GetCurrentTime()
    {
        return (time % (uint)timeMax) / timeMax;
    }

    public float GetCurrentTime(float angle)
    {
        float timeLocal = time + (angle - 0.5F) * timeMax;
        return (float)((uint)Mathf.RoundToInt(timeLocal + timeMax) % (uint)timeMax) / timeMax;
    }

    public int GetCurrentHour(float time)
    {
        return Mathf.RoundToInt(((time * timeMax) / DEFAULT_INT_SECONDS_IN_HOURS));
    }

    public int GetCurrentMinute(float time)
    {
        return Mathf.RoundToInt(((time * timeMax) / DEFAULT_INT_SECONDS_IN_MINUTES) % DEFAULT_INT_MINUTES_IN_HOURS);
    }

    public int GetCurrentSecond(float time)
    {
        return Mathf.RoundToInt((time * timeMax) % DEFAULT_INT_SECONDS_IN_MINUTES);
    }
}
