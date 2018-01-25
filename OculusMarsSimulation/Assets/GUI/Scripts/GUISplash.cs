using UnityEngine;
using UnityEngine.UI;

public class GUISplash : Animatable
{
    private Image guiBackground;

    public void Start()
    {
        guiBackground = GetComponent<Image>();    
    }

    public void LateUpdate()
    {
        if (guiBackground != null)
        {
            switch (animationType)
            {
                case "fadein":
                    guiBackground.color = new Color(1, 1, 1, animationCurrent);
                    break;

                case "fadeout":
                    guiBackground.color = new Color(1, 1, 1, 1 - animationCurrent);
                    break;
            }
        }
    }
}
