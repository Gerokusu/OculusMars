using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static readonly string DEFAULT_STRING_INPUT_MOVE_HORIZONTAL = "MoveHorizontal";
    public static readonly string DEFAULT_STRING_INPUT_MOVE_VERTICAL = "MoveVertical";

    public uint speedWalking = 1;

	public void Update()
    {
        transform.Translate(new Vector3(Input.GetAxis(DEFAULT_STRING_INPUT_MOVE_HORIZONTAL) * speedWalking, 0, Input.GetAxis(DEFAULT_STRING_INPUT_MOVE_VERTICAL) * speedWalking) * Time.deltaTime);
	}
}
