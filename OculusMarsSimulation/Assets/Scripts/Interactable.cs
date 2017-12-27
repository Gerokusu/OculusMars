using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public List<Animatable> animatables;

    public bool CanInteract()
    {
        bool canInteract = true;
        if(animatables != null)
        {
            foreach (Animatable animatable in animatables)
            {
                canInteract = canInteract && animatable != null && !animatable.isAnimating;
            }
        }

        return canInteract;
    }
}
