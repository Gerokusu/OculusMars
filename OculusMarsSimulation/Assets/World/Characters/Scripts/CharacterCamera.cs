using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    public static readonly string DEFAULT_STRING_INPUT_LOOK_HORIZONTAL = "LookHorizontal";
    public static readonly string DEFAULT_STRING_INPUT_LOOK_VERTICAL = "LookVertical";

    public Transform cameraTransform;
    public uint cameraSensivity = 1;

	public void Update()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis(DEFAULT_STRING_INPUT_LOOK_HORIZONTAL) * cameraSensivity, 0) * Time.deltaTime);
        if(cameraTransform != null)
        {
            cameraTransform.Rotate(new Vector3(Input.GetAxis(DEFAULT_STRING_INPUT_LOOK_VERTICAL) * -cameraSensivity, 0, 0) * Time.deltaTime);
        }
	}
}
