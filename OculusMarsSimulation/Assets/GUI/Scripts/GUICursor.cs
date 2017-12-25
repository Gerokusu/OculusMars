using UnityEngine;
using UnityEngine.UI;

public class GUICursor : MonoBehaviour
{
    public Transform target;
    public RectTransform guiBackground;
    public Text guiTextCaption;

    public void Start()
    {

    }
	
	public void Update()
    {
        if(target != null)
        {
            
            RectTransform canvas = transform.parent.GetComponent<RectTransform>();
            Vector2 positionViewport = Camera.main.WorldToViewportPoint(target.position);
            Vector2 position = new Vector2(positionViewport.x * canvas.sizeDelta.x, positionViewport.y * canvas.sizeDelta.y) - new Vector2(canvas.sizeDelta.x / 2.0F, canvas.sizeDelta.y / 2.0F);
            transform.localPosition = new Vector3(position.x, position.y, transform.localPosition.z);
            
            //transform.position = target.position;
        }

        if(guiBackground != null && guiTextCaption != null)
        {
            float width = LayoutUtility.GetPreferredWidth(guiTextCaption.rectTransform);
            float offset = guiTextCaption.rectTransform.localPosition.x;
            guiBackground.sizeDelta = new Vector2(width + offset * 2, guiBackground.sizeDelta.y);
            guiTextCaption.rectTransform.sizeDelta = new Vector2(width, guiTextCaption.rectTransform.sizeDelta.y);
        }
    }
}
