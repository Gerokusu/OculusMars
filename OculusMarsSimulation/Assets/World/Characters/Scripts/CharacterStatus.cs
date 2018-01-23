using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour
{
    public static readonly string DEFAULT_STRING_GUICAPTION_TIME_FORMAT = "{0}:{1}:{2}";
    public static readonly string DEFAULT_STRING_GUICAPTION_PRSS_FORMAT = "{0}bar";
    public static readonly string DEFAULT_STRING_GUICAPTION_MASS_FORMAT = "{0}KG";
    public static readonly string DEFAULT_STRING_GUICAPTION_ITEM_FORMAT = "{0}";
    public static readonly string DEFAULT_STRING_GUICAPTION_ITEMMASS_FORMAT = "<{0} KG>";

    public Text guiCaptionHour;
    public Text guiCaptionItemName;
    public Text guiCaptionItemMass;
    public GUICursor guiCursorItemAimed;

    public PlanetTime planetTime;

    private float time;

    private CharacterInteraction characterInteraction;

    public void Start()
    {
        characterInteraction = GetComponent<CharacterInteraction>();
    }
    
    public void Update()
    {
        if(planetTime != null)
        {
            time = planetTime.GetCurrentTime();
        }

        //uint massItem = (characterInteraction != null && characterInteraction.itemGrabbed != null) ? characterInteraction.itemGrabbed.mass : 0;
        //oxygen -= (oxygen > 0) ? oxygenConsumptionPerSecond * Time.deltaTime : 0;

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

        if (guiCursorItemAimed != null && characterInteraction != null)
        {
            Interactable target = null;
            if (characterInteraction.triggerAimed != null)
            {
                target = characterInteraction.triggerAimed;
            }
            else if (characterInteraction.itemAimed != null)
            {
                target = characterInteraction.itemAimed;
            }
            
            if (target != null && target.CanInteract())
            {
                guiCursorItemAimed.gameObject.SetActive(true);
                guiCursorItemAimed.target = target.transform;
                guiCursorItemAimed.guiBackground.SetCaption(string.Format(DEFAULT_STRING_GUICAPTION_ITEM_FORMAT, target.title));
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
