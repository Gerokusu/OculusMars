using UnityEngine;

public class PlanetSpin : MonoBehaviour
{
    public int RotationSpeed = 1;

	public void FixedUpdate()
    {
        transform.Rotate(0, -RotationSpeed * Time.deltaTime, 0);
	}
}
