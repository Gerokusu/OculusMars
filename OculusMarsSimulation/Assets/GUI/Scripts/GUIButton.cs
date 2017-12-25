using UnityEngine;

public class GUIButton : AnimatableBehaviour
{
    public override void OnAnimationWork()
    {
        transform.localPosition = positionInitial - transform.forward * Mathf.Sin(animationCurrent * Mathf.PI) * animationAmplitude;
    }

    public override void OnAnimationEnd()
    {
        transform.localPosition = positionInitial;
    }
}
