using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIMission : Interactable
{
    public static readonly char DEFAULT_STRING_GUITEXT_DIFFICULTY_CHAR = '✦';
    public static readonly string DEFAULT_STRING_GUITEXT_NUMBER_FORMAT = "#{0}";
    public static readonly string DEFAULT_STRING_GUICURSOR_NAME = "GUICursor{0}";
    public static readonly string DEFAULT_STRING_GUICURSOR_CAPTION_FORMAT = "//{0}";
    public static readonly string DEFAULT_STRING_GUIBUTTONLAUNCH_CAPTION_AVAILABLE = "Launch !";
    public static readonly string DEFAULT_STRING_GUIBUTTONLAUNCH_CAPTION_UNAVAILABLE = "//ERROR_";
    public static readonly Color DEFAULT_STRING_GUIBUTTONLAUNCH_COLOR_AVAILABLE = new Color(1, 1, 1, 1);
    public static readonly Color DEFAULT_STRING_GUIBUTTONLAUNCH_COLOR_UNAVAILABLE = new Color(0.5F, 0.5F, 0.5F, 1);

    public Text guiTextTitle;
    public Text guiTextID;
    public Text guiTextDescription;
    public Text guiTextDifficulty;

    public GUIButton guiButtonPrevious;
    public GUIButton guiButtonNext;
    public GUIButton guiButtonLaunch;

    public GUICursor guiCursorPrefab;
    public GUICursor[] guiCursors;

    public GUISplash guiSplashLoading;

    public PlanetNavigator planetNavigator;

    private int sceneToLoad;

    public void Start()
    {
        animatables.AddRange(new Animatable[] { planetNavigator, guiButtonPrevious, guiButtonNext, guiButtonLaunch, guiSplashLoading });

        if (planetNavigator != null && planetNavigator.planetHub != null && guiCursorPrefab != null)
        {
            List<GUICursor> guiCursorsInstances = new List<GUICursor>();
            foreach (PlanetMission planetMission in planetNavigator.planetHub.GetComponentsInChildren<PlanetMission>())
            {
                GUICursor cursor = Instantiate(guiCursorPrefab);
                cursor.transform.SetParent(transform.parent, false);
                cursor.name = string.Format(DEFAULT_STRING_GUICURSOR_NAME, planetMission.mission.locationName.ToUpper().Replace(" ", ""));
                cursor.target = planetMission.transform;
                if(cursor.guiBackground != null)
                {
                    Debug.Log(string.Format(DEFAULT_STRING_GUICURSOR_CAPTION_FORMAT, planetMission.mission.locationName.ToUpper().Replace(" ", "_")));
                    cursor.guiBackground.SetCaption(string.Format(DEFAULT_STRING_GUICURSOR_CAPTION_FORMAT, planetMission.mission.locationName.ToUpper().Replace(" ", "_")));
                }
                guiCursorsInstances.Add(cursor);
            }

            guiSplashLoading.transform.SetAsLastSibling();

            guiCursors = guiCursorsInstances.ToArray();
        }

        sceneToLoad = -1;
    }
	
	public void Update()
    {
        if (planetNavigator.selectedMission != null && CanInteract())
        {
            if (sceneToLoad < 0)
            {
                int shift = Mathf.RoundToInt(Input.GetAxis("FormNavigate"));
                if (shift != 0)
                {
                    planetNavigator.SetMission(planetNavigator.selectedMissionIndex + shift);

                    GUIButton pressed = (shift < 0) ? guiButtonPrevious : guiButtonNext;
                    if (pressed != null)
                    {
                        pressed.direction = (shift < 0) ? pressed.transform.forward : new Vector3(0, 0, 1);
                        pressed.Animate();
                    }
                }

                if (Input.GetButtonDown("FormAccept"))
                {
                    if (planetNavigator.selectedMission.mission.locationScene < SceneManager.sceneCountInBuildSettings)
                    {
                        guiButtonLaunch.Animate();
                        guiSplashLoading.Animate("fadein");
                        sceneToLoad = planetNavigator.selectedMission.mission.locationScene;
                    }
                }
            }
            else
            {
                SceneManager.LoadSceneAsync(sceneToLoad);
                sceneToLoad = -1;
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

            if(guiButtonLaunch != null && guiButtonLaunch.caption != null)
            {
                bool sceneExists = planetNavigator.selectedMission.mission.locationScene < SceneManager.sceneCountInBuildSettings;
                guiButtonLaunch.caption.text = (sceneExists) ? DEFAULT_STRING_GUIBUTTONLAUNCH_CAPTION_AVAILABLE : DEFAULT_STRING_GUIBUTTONLAUNCH_CAPTION_UNAVAILABLE;

                foreach(Image guiImage in guiButtonLaunch.GetComponentsInChildren<Image>())
                {
                    guiImage.color = (sceneExists) ? DEFAULT_STRING_GUIBUTTONLAUNCH_COLOR_AVAILABLE : DEFAULT_STRING_GUIBUTTONLAUNCH_COLOR_UNAVAILABLE;
                }

                foreach(Text guiText in guiButtonLaunch.GetComponentsInChildren<Text>())
                {
                    guiText.color = (sceneExists) ? new Color(guiText.color.r, guiText.color.g, guiText.color.b, 1) : new Color(guiText.color.r, guiText.color.g, guiText.color.b, 0.5F);
                }
            }
        }
    }
}
