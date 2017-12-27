using UnityEngine;

public class GUIButton : Animatable
{
    public Vector3 direction;

    public void Start()
    {
        direction = transform.forward;
    }

    public void LateUpdate()
    {
        if (isAnimating)
        {
            transform.localPosition = positionInitial - direction * Mathf.Sin(animationCurrent * Mathf.PI) * animationAmplitude;
        }
    }
}
