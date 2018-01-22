using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour
{
    public static readonly string DEFAULT_STRING_GUICAPTION_TIME_FORMAT = "{0}:{1}:{2}";
    public static readonly string DEFAULT_STRING_GUICAPTION_TEMP_FORMAT = "{0}°C";
    public static readonly string DEFAULT_STRING_GUICAPTION_PRSS_FORMAT = "{0}bar";
    public static readonly string DEFAULT_STRING_GUICAPTION_MASS_FORMAT = "{0}KG";
    public static readonly string DEFAULT_STRING_GUICAPTION_ITEM_FORMAT = "{0} <{1} KG>";
    public static readonly string DEFAULT_STRING_GUICAPTION_ITEMMASS_FORMAT = "<{0} KG>";

    public Text guiCaptionHour;
    public Text guiCaptionTemp;
    public Text guiCaptionPressure;
    public Text guiCaptionMass;
    public Image guiCaptionOxygen;
    public Text guiCaptionItemName;
    public Text guiCaptionItemMass;
    public GUICursor guiCursorItemAimed;

    public PlanetTime planetTime;

    private float time;
    public float temperature;
    public float pressure;
    public float mass;
    private float oxygen;
    private float oxygenMaximum;

    [Range(0, 1)]
    public float oxygenConsumptionPerSecond;

    private CharacterInteraction characterInteraction;

    public void Start()
    {
        oxygen = 1;
        oxygenMaximum = (guiCaptionOxygen != null) ? guiCaptionOxygen.rectTransform.sizeDelta.y : 0;

        characterInteraction = GetComponent<CharacterInteraction>();
    }
    
    public void Update()
    {
        if(planetTime != null)
        {
            time = planetTime.GetCurrentTime();
        }
        oxygen -= (oxygen > 0) ? oxygenConsumptionPerSecond * Time.deltaTime : 0;

        Refresh();
    }

    public void Refresh()
    {
        if (guiCaptionHour != null && planetTime != null)
        {
            string hourUnit = Mathf.RoundToInt(planetTime.GetCurrentHour(time)).ToString("00");
            string minuteUnit = Mathf.RoundToInt(planetTime.GetCurrentMinute(time)).ToString("00");
            string secondUnit = Mathf.RoundToInt(planetTime.GetCurrentSecond(time)).ToString("00");
            guiCaptionHour.text = string.Format(DEFAULT_STRING_GUICAPTION_TIME_FORMAT, hourUnit, minuteUnit, secondUnit);
        }

        if (guiCaptionTemp != null)
        {
            string temperatureSign = (temperature >= 0) ? "+" : "";
            string temperatureUnit = string.Format("{0:00}", Mathf.RoundToInt(temperature));
            guiCaptionTemp.text = string.Format(DEFAULT_STRING_GUICAPTION_TEMP_FORMAT, temperatureSign + temperatureUnit);
        }

        if (guiCaptionPressure != null)
        {
            string pressureUnit = string.Format("{0:0.000}", pressure);
            guiCaptionPressure.text = string.Format(DEFAULT_STRING_GUICAPTION_PRSS_FORMAT, pressureUnit);
        }

        if (guiCaptionMass != null)
        {
            uint massItem = (characterInteraction != null && characterInteraction.itemGrabbed != null) ? characterInteraction.itemGrabbed.mass : 0;

            string massUnit = string.Format("{0:00}", Mathf.RoundToInt(mass + massItem));
            guiCaptionMass.text = string.Format(DEFAULT_STRING_GUICAPTION_MASS_FORMAT, massUnit);
        }

        if (guiCaptionOxygen != null)
        {
            float height = (oxygen > 0) ? oxygen * oxygenMaximum : 0;
            guiCaptionOxygen.rectTransform.sizeDelta = new Vector2(guiCaptionOxygen.rectTransform.sizeDelta.x, Mathf.RoundToInt(height));
        }

        if (guiCursorItemAimed != null && characterInteraction != null)
        {
            if(characterInteraction.itemAimed != null)
            {
                guiCursorItemAimed.gameObject.SetActive(true);
                guiCursorItemAimed.target = characterInteraction.itemAimed.transform;
                guiCursorItemAimed.guiBackground.SetCaption(string.Format(DEFAULT_STRING_GUICAPTION_ITEM_FORMAT, characterInteraction.itemAimed.title, characterInteraction.itemAimed.mass));
            }
            else
            {
                guiCursorItemAimed.gameObject.SetActive(false);
                guiCursorItemAimed.target = null;
            }
        }

        if (guiCaptionItemName != null && characterInteraction != null)
        {
            string name = (characterInteraction.itemGrabbed != null) ? characterInteraction.itemGrabbed.title : "";
            guiCaptionItemName.text = name;
        }

        if(guiCaptionItemMass != null && characterInteraction != null)
        {
            string mass = (characterInteraction.itemGrabbed != null) ? string.Format(DEFAULT_STRING_GUICAPTION_ITEMMASS_FORMAT, characterInteraction.itemGrabbed.mass) + "" : "<>";
            guiCaptionItemMass.text = mass;
        }
    }
}
