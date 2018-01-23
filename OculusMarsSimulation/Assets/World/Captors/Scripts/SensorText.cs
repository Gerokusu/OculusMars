using UnityEngine;
using UnityEngine.UI;

public class SensorText : Sensor
{
    public string format;
    public bool isSigned = false;
    public Text guiCaption;

    public override void Display()
    {
        if(guiCaption != null)
        {
            guiCaption.text = string.Format(format, value);
        }
    }
}
