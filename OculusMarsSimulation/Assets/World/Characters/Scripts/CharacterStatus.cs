using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour
{
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

    [Range(0, 1)]
    public float consumptionOxygenPerSecond;

    private int hour;
    private int minute;
    private int second;
    private float temperature;
    private float pressure;
    private float mass;
    private float oxygen;
    private float oxygenMaximum;

    private CharacterInteraction characterInteraction;

    public void Start()
    {
        oxygen = 1;
        oxygenMaximum = (guiCaptionOxygen != null) ? guiCaptionOxygen.rectTransform.sizeDelta.y : 0;

        characterInteraction = GetComponent<CharacterInteraction>();
    }
    
    public void Update()
    {
        oxygen -= (oxygen > 0) ? consumptionOxygenPerSecond * Time.deltaTime : 0;

        Refresh();
    }

    public void Refresh()
    {
        if(guiCaptionOxygen != null)
        {
            float height = (oxygen > 0) ? oxygen * oxygenMaximum : 0;
            guiCaptionOxygen.rectTransform.sizeDelta = new Vector2(guiCaptionOxygen.rectTransform.sizeDelta.x, height);
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
