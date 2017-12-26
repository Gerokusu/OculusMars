using UnityEngine;

public class PlanetSpin : MonoBehaviour
{
    public int rotationSpeed = 1;

    public void FixedUpdate()
    {
        transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
    }
}
