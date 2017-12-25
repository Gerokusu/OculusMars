using UnityEngine;

public class GUIButton : MonoBehaviour
{
    public bool isAnimating = false;

    private Vector3 positionInitial;
    private float animationCurrent;
    private float animationLength;
    private float animationAmplitude;

    public void Update()
    {
        if (isAnimating)
        {
            animationCurrent += (Time.deltaTime / animationLength);
            if(animationCurrent < 1)
            {
                transform.localPosition = positionInitial - transform.forward * Mathf.Sin(animationCurrent * Mathf.PI) * animationAmplitude;
            }
            else
            {
                isAnimating = false;
                transform.localPosition = positionInitial;
            }
        }
    }

    public void AnimatePress(float length, float amplitude)
    {
        if(!isAnimating)
        {
            isAnimating = true;
            positionInitial = transform.localPosition;
            animationCurrent = 0;
            animationLength = (length > 0) ? length : 1;
            animationAmplitude = amplitude;
        }
    }
}
