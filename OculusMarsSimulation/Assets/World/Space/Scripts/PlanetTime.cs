using UnityEngine;

public class PlanetTime : MonoBehaviour
{
    public const uint DEFAULT_INT_SECONDS_IN_HOURS = 3600;
    public const uint DEFAULT_INT_SECONDS_IN_MINUTES = 60;
    public const uint DEFAULT_INT_MINUTES_IN_HOURS = 60;
    
    public uint dayHours = 24;
    public uint dayMinutes = 0;
    public uint daySeconds = 0;

    private ulong time;
    private ulong timeMax;

    public void Start()
    {
        timeMax = dayHours * DEFAULT_INT_SECONDS_IN_HOURS + dayMinutes * DEFAULT_INT_SECONDS_IN_MINUTES + daySeconds;
    }

    public void Update()
    {
        float rotation = (360 - transform.localRotation.eulerAngles.y) / 360;
        time = (ulong)Mathf.FloorToInt(rotation * timeMax + timeMax) % (uint)timeMax;
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
