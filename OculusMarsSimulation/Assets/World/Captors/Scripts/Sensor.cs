using UnityEngine;

public abstract class Sensor : MonoBehaviour
{
    public SensorType type;
    public float value;
    public float depletion;

    public void Update()
    {
        value += depletion * Time.deltaTime;

        Display();
    }

    public abstract void Display();
}
