using UnityEngine;

public class Animatable : MonoBehaviour
{
    public bool isAnimating = false;

    protected Vector3 positionInitial;
    protected Vector3 rotationInitial;

    protected float animationCurrent;
    public float animationLength;
    public float animationAmplitude;
    public float animationAcceleration = 1;

    public void Start()
    {
        OnStart();
    }

    public void Update()
    {
        if (isAnimating && animationLength > 0)
        {
            animationCurrent += Time.deltaTime / animationLength * animationAcceleration;
            if(animationCurrent < 1)
            {
                OnAnimationWork();
            }
            else
            {
                isAnimating = false;
                OnAnimationEnd();
            }
        }

        OnUpdate();
    }

    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
    public virtual void OnAnimationWork() { }
    public virtual void OnAnimationEnd() { }

    public void Animate()
    {
        if(!isAnimating)
        {
            isAnimating = true;
            positionInitial = transform.localPosition;
            rotationInitial = transform.localRotation.eulerAngles;
            animationCurrent = 0;
        }
    }
}
