using UnityEngine;
using UnityEngine.UI;

public class GUICursor : Animatable
{
    public Transform target;
    public RectTransform guiBackground;
    public Text guiTextCaption;

    public bool isFaded = false;
    
	public override void OnUpdate()
    {
        if(target != null)
        {
            RectTransform canvas = transform.parent.GetComponent<RectTransform>();
            Vector2 positionViewport = Camera.main.WorldToViewportPoint(target.position);
            Vector2 position = new Vector2(positionViewport.x * canvas.sizeDelta.x, positionViewport.y * canvas.sizeDelta.y) - new Vector2(canvas.sizeDelta.x / 2.0F, canvas.sizeDelta.y / 2.0F);
            transform.localPosition = new Vector3(position.x, position.y, transform.localPosition.z);
        }

        if(guiBackground != null && guiTextCaption != null)
        {
            float width = LayoutUtility.GetPreferredWidth(guiTextCaption.rectTransform);
            float offset = guiTextCaption.rectTransform.localPosition.x;
            guiBackground.sizeDelta = new Vector2(width + offset * 2, guiBackground.sizeDelta.y);
            guiTextCaption.rectTransform.sizeDelta = new Vector2(width, guiTextCaption.rectTransform.sizeDelta.y);
        }
    }

    public override void OnAnimationWork()
    {
        float value = isFaded ? animationCurrent : 1 - animationCurrent;

        Image image = GetComponent<Image>();
        if(image != null)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, value);
        }

        Image imageBackground = guiBackground.GetComponent<Image>();
        if (imageBackground != null)
        {
            imageBackground.color = new Color(imageBackground.color.r, imageBackground.color.g, imageBackground.color.b, value);
        }

        if (guiTextCaption != null)
        {
            guiTextCaption.color = new Color(guiTextCaption.color.r, guiTextCaption.color.g, guiTextCaption.color.b, value);
        }
    }

    public override void OnAnimationEnd()
    {
        isFaded = !isFaded;
    }
}
