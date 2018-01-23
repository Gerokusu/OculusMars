using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static readonly string DEFAULT_STRING_INPUT_MOVE_HORIZONTAL = "MoveHorizontal";
    public static readonly string DEFAULT_STRING_INPUT_MOVE_VERTICAL = "MoveVertical";
    public static readonly string DEFAULT_STRING_INPUT_LOOK_HORIZONTAL = "LookHorizontal";
    public static readonly string DEFAULT_STRING_INPUT_LOOK_VERTICAL = "LookVertical";

    public uint walkingSpeed = 1;
    private float walkingAnimation = 0;

    public Transform cameraTransform;
    public uint cameraSensivity = 1;

    public Transform controllerLeft;
    public Transform controllerRight;

    public void Update()
    {
        Vector3 translation = new Vector3(Input.GetAxis(DEFAULT_STRING_INPUT_MOVE_HORIZONTAL) * walkingSpeed, 0, Input.GetAxis(DEFAULT_STRING_INPUT_MOVE_VERTICAL) * walkingSpeed);
        transform.Translate(translation * Time.deltaTime);
        transform.Rotate(new Vector3(0, Input.GetAxis(DEFAULT_STRING_INPUT_LOOK_HORIZONTAL) * cameraSensivity, 0) * Time.deltaTime);

        if (cameraTransform != null)
        {
            walkingAnimation += Time.deltaTime * walkingSpeed / 2.0F;

            float walkingAnimationCurrent = (Mathf.Sin(walkingAnimation * Mathf.PI) + 0.5F) * translation.magnitude * 0.03F; ;
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, walkingAnimationCurrent, cameraTransform.localPosition.z);
            if(!OVRManager.isHmdPresent)
            {
                cameraTransform.Rotate(new Vector3(Input.GetAxis(DEFAULT_STRING_INPUT_LOOK_VERTICAL) * -cameraSensivity, 0, 0) * Time.deltaTime);
            }
        }
    }
}
