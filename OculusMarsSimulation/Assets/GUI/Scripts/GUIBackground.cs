using UnityEngine;
using UnityEngine.UI;

public class GUIBackground : MonoBehaviour
{
    private Image guiImage;
    private Text guiCaption;

    public bool adaptImageToText = true;

    public void Start()
    {
        guiImage = GetComponent<Image>();
        guiCaption = GetComponentInChildren<Text>();
    }

    public void LateUpdate()
    {
        if (guiImage != null && guiCaption != null)
        {
            float offset = guiCaption.rectTransform.localPosition.x;
            float width = LayoutUtility.GetPreferredWidth(guiCaption.rectTransform);
            guiCaption.rectTransform.sizeDelta = new Vector2(width, guiCaption.rectTransform.sizeDelta.y);
            if (!adaptImageToText)
            {
                guiImage.rectTransform.sizeDelta = new Vector2(width + offset * 2, guiImage.rectTransform.sizeDelta.y);
            }
        }
    }

    public void SetCaption(string text)
    {
        if (guiCaption != null)
        {
            guiCaption.text = text;
        }
    }
}
