using System;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GUIMission : MonoBehaviour
{
    public const uint DEFAULT_SIZE_WIDTH_MIN = 300;
    public const uint DEFAULT_SIZE_HEIGHT_MIN = 300;
    public const uint DEFAULT_SIZE_WIDTH_MAX = 1000;
    public const uint DEFAULT_SIZE_HEIGHT_MAX = 1000;
    public const string DEFAULT_GUITEXT_NUMBER_FORMAT = "#{0}";
    public const char DEFAULT_GUITEXT_DIFFICULTY_CHAR = '✦';

    public RectTransform rectTransform;
    public Text guiTextTitle;
    public Text guiTextID;
    public Text guiTextDescription;
    public Text guiTextDifficulty;

    [Range(0, 1)]
    public float screenX = 0.5F;
    [Range(0, 1)]
    public float screenY = 0.5F;
    public Vector2 minSize = new Vector2(DEFAULT_SIZE_WIDTH_MIN, DEFAULT_SIZE_HEIGHT_MIN);
    public Vector2 maxSize = new Vector2(DEFAULT_SIZE_WIDTH_MAX, DEFAULT_SIZE_HEIGHT_MAX);

    public Mission selectedMission;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
	
	public void Update()
    {
        if(rectTransform != null)
        {
            rectTransform.sizeDelta = new Vector2(Mathf.Clamp(rectTransform.sizeDelta.x, minSize.x, maxSize.x), Mathf.Clamp(rectTransform.sizeDelta.y, minSize.y, maxSize.y));
            rectTransform.position = new Vector3(Screen.width * screenX - rectTransform.sizeDelta.x / 2, Screen.height * screenY - rectTransform.sizeDelta.y / 2);

            if(selectedMission != null)
            {
                if(guiTextTitle != null)
                {
                    guiTextTitle.text = selectedMission.locationName;
                }

                if (guiTextID != null)
                {
                    guiTextID.text = string.Format(DEFAULT_GUITEXT_NUMBER_FORMAT, selectedMission.id.ToString("000"));
                }

                if (guiTextDescription != null)
                {
                    guiTextDescription.text = selectedMission.description;
                }

                if (guiTextDifficulty != null)
                {
                    guiTextDifficulty.text = new String(DEFAULT_GUITEXT_DIFFICULTY_CHAR, (int)selectedMission.difficulty);
                }
            }
        }
	}
}
