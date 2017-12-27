using System;
using UnityEngine;
using UnityEngine.UI;

public class GUIMission : Interactable
{
    public const char DEFAULT_STRING_GUITEXT_DIFFICULTY_CHAR = '✦';
    public const string DEFAULT_STRING_GUITEXT_NUMBER_FORMAT = "#{0}";
    public const string DEFAULT_STRING_GUICURSOR_NAME = "GUICursor{0}";
    public const string DEFAULT_STRING_GUICURSOR_CAPTION_FORMAT = "//{0}";
    
    public Text guiTextTitle;
    public Text guiTextID;
    public Text guiTextDescription;
    public Text guiTextDifficulty;

    public GUIButton guiButtonPrevious;
    public GUIButton guiButtonNext;
    public GUIButton guiButtonLaunch;

    public GUICursor guiCursorPrefab;

    public PlanetNavigator planetNavigator;

    public void Start()
    {
        animatables.AddRange(new Animatable[] { planetNavigator, guiButtonPrevious, guiButtonNext, guiButtonLaunch });

        if (planetNavigator != null && planetNavigator.planetHub != null && guiCursorPrefab != null)
        {
            foreach (PlanetMission planetMission in planetNavigator.planetHub.GetComponentsInChildren<PlanetMission>())
            {
                GUICursor cursor = Instantiate(guiCursorPrefab);
                cursor.transform.SetParent(transform.parent, false);
                cursor.name = string.Format(DEFAULT_STRING_GUICURSOR_NAME, planetMission.mission.locationName.ToUpper().Replace(" ", ""));
                cursor.target = planetMission.transform;
                cursor.SetCaption(string.Format(DEFAULT_STRING_GUICURSOR_CAPTION_FORMAT, planetMission.mission.locationName.ToUpper().Replace(" ", "_")));
            }
        }
    }
	
	public void Update()
    {
        if (planetNavigator.selectedMission != null && CanInteract())
        {
            int shift = Mathf.RoundToInt(Input.GetAxis("FormNavigate"));
            if (shift != 0)
            {
                planetNavigator.SetMission(planetNavigator.selectedMissionIndex + shift);

                GUIButton pressed = (shift < 0) ? guiButtonPrevious : guiButtonNext;
                if (pressed != null)
                {
                    RectTransform canvas = transform.parent.GetComponent<RectTransform>();
                    pressed.direction = (shift < 0) ? pressed.transform.forward : new Vector3(0, 0, 1);
                    pressed.Animate();
                }
            }

            if (Input.GetButtonDown("FormAccept"))
            {
                guiButtonLaunch.Animate();
            }
        }

        Refresh();
	}
    
    public void Refresh()
    {
        if (planetNavigator.selectedMission != null)
        {
            if (guiTextTitle != null)
            {
                guiTextTitle.text = planetNavigator.selectedMission.mission.locationName;
            }

            if (guiTextID != null)
            {
                guiTextID.text = string.Format(DEFAULT_STRING_GUITEXT_NUMBER_FORMAT, planetNavigator.selectedMission.mission.id.ToString("000"));
            }

            if (guiTextDescription != null)
            {
                guiTextDescription.text = planetNavigator.selectedMission.mission.description;
            }

            if (guiTextDifficulty != null)
            {
                guiTextDifficulty.text = new String(DEFAULT_STRING_GUITEXT_DIFFICULTY_CHAR, (int)planetNavigator.selectedMission.mission.difficulty);
            }
        }
    }
}
