using UnityEngine;
using UnityEngine.UI;

public class GUICursor : MonoBehaviour
{
    public GUIBackground guiBackground;

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
}
