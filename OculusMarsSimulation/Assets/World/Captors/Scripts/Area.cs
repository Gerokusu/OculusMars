using UnityEngine;

public class Area : Animatable
{
    public float temperature;
    public float pressure;
    public float radiation;
    public float oxygenDepletion;

    public Area areaInterior;
    public Area areaExterior;

    public void FixedUpdate()
    {
        animationCurrent = Mathf.Clamp(animationCurrent, 0, 1);

        if(areaInterior != null && areaExterior != null)
        {
            Area areaInitial = (isAnimating && animationType == "tointerior") ? areaExterior : areaInterior;
            Area areaObjective = (isAnimating && animationType == "tointerior") ? areaInterior : areaExterior;
            Debug.Log(Mathf.Lerp(areaInitial.temperature, areaObjective.temperature, animationCurrent));
            temperature = Mathf.Lerp(areaInitial.temperature, areaObjective.temperature, animationCurrent);
            pressure = Mathf.Lerp(areaInitial.pressure, areaObjective.pressure, animationCurrent);
            radiation = Mathf.Lerp(areaInitial.radiation, areaObjective.radiation, animationCurrent);
            oxygenDepletion = Mathf.Lerp(areaInitial.oxygenDepletion, areaObjective.oxygenDepletion, animationCurrent);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        Sensor[] sensors = other.GetComponents<Sensor>();
        if(sensors != null && sensors.Length > 0)
        {
            foreach(Sensor sensor in sensors)
            {
                switch(sensor.type)
                {
                    case SensorType.Temperature:
                        sensor.value = temperature;
                        break;

                    case SensorType.Pressure:
                        sensor.value = pressure;
                        break;

                    case SensorType.Radiation:
                        sensor.value = radiation;
                        break;

                    case SensorType.Oxygen:
                        sensor.depletion = oxygenDepletion;
                        break;
                }
            }
        }
    }
}
