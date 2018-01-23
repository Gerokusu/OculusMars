using System;
using System.Collections.Generic;
using UnityEngine;

public class Animatable : MonoBehaviour
{
    public bool isAnimating = false;

    protected Vector3 positionInitial;
    protected Vector3 rotationInitial;

    public float animationLength;
    public float animationAmplitude;
    public float animationAcceleration = 1;
    public string animationType;

    public float animationCurrent;

    private List<Action> callbacks = new List<Action>();
    
    public void Update()
    {
        if (isAnimating && animationLength > 0)
        {
            animationCurrent += Time.deltaTime / animationLength * animationAcceleration;
            if(animationCurrent >= 1)
            {
                isAnimating = false;
                transform.localPosition = positionInitial;
                transform.localRotation = Quaternion.Euler(rotationInitial);
                for(int i = 0; i < callbacks.Count; i++)
                {
                    Action callback = callbacks[i];
                    if(callback != null)
                    {
                        callbacks[i]();
                    }
                }

                callbacks.Clear();
            }
        }
    }

    public void Animate(string type = "", Action callback = null)
    {
        if(!isAnimating)
        {
            isAnimating = true;
            positionInitial = transform.localPosition;
            rotationInitial = transform.localRotation.eulerAngles;
            animationCurrent = 0;
            animationType = type;

            if(callback != null)
            {
                callbacks.Add(callback);
            }
        }
    }
}
