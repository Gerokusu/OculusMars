using UnityEngine;
using UnityEngine.UI;

public class GUICursor : MonoBehaviour
{
    public Image guiBackground;
    public Text guiCaption;

    public Transform target;

    public void LateUpdate()
    {
        RectTransform canvas = transform.parent.GetComponent<RectTransform>();
        if (canvas != null && target != null)
        {
            Vector2 positionViewport = Camera.main.WorldToViewportPoint(target.position);
            Vector2 position = new Vector2(positionViewport.x * canvas.sizeDelta.x, positionViewport.y * canvas.sizeDelta.y) - new Vector2(canvas.sizeDelta.x / 2.0F, canvas.sizeDelta.y / 2.0F);
            transform.localPosition = new Vector3(position.x, position.y, transform.localPosition.z);
        }
    }

    public void SetCaption(string text)
    {
        if (guiBackground != null && guiCaption != null)
        {
            guiCaption.text = text;

            float width = LayoutUtility.GetPreferredWidth(guiCaption.rectTransform);
            float offset = guiCaption.rectTransform.localPosition.x;
            guiBackground.rectTransform.sizeDelta = new Vector2(width + offset * 2, guiBackground.rectTransform.sizeDelta.y);
            guiCaption.rectTransform.sizeDelta = new Vector2(width, guiCaption.rectTransform.sizeDelta.y);
        }
    }
}
