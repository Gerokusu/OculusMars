using UnityEngine;
using UnityEngine.UI;

public class SensorBar : Sensor
{
    public Image guiBar;

    private float valueMaximum;

    public void Start()
    {
        valueMaximum = (guiBar != null) ? guiBar.rectTransform.sizeDelta.y : 0;
    }

    public override void Display()
    {
        if (guiBar != null)
        {
            value = Mathf.Clamp(value, 0, 1);

            float height = (value > 0) ? value * valueMaximum : 0;
            guiBar.rectTransform.sizeDelta = new Vector2(guiBar.rectTransform.sizeDelta.x, Mathf.RoundToInt(height));
        }
    }
}
