using System;
using UnityEngine;
using UnityEngine.UI;

public class GUIMission : MonoBehaviour
{
    public const string DEFAULT_GUITEXT_NUMBER_FORMAT = "#{0}";
    public const char DEFAULT_GUITEXT_DIFFICULTY_CHAR = '✦';
    public const string DEFAULT_GUICURSOR_NAME = "GUICursor{0}";
    public const string DEFAULT_GUICURSOR_CAPTION_FORMAT = "//{0}";

    public RectTransform rectTransform;
    public Text guiTextTitle;
    public Text guiTextID;
    public Text guiTextDescription;
    public Text guiTextDifficulty;
    public GUIButton guiButtonPrevious;
    public GUIButton guiButtonNext;
    public GUIButton guiButtonLaunch;
    public float guiButtonPressAmplitude;

    public GUICursor guiCursorPrefab;

    public Navigator cameraNavigator;
    public PlanetHub planet;
    public int selectedMissionIndex;
    public PlanetMission selectedMission;

    private bool isShifting = false;
    private bool isLoading = false;
    private bool isFinished = false;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (planet != null && guiCursorPrefab != null)
        {
            foreach (PlanetMission planetMission in planet.GetComponentsInChildren<PlanetMission>())
            {
                GUICursor cursor = Instantiate(guiCursorPrefab);
                cursor.transform.SetParent(transform.parent, false);
                cursor.name = string.Format(DEFAULT_GUICURSOR_NAME, planetMission.mission.locationName.ToUpper().Replace(" ", ""));
                cursor.target = planetMission.transform;
                cursor.guiTextCaption.text = string.Format(DEFAULT_GUICURSOR_CAPTION_FORMAT, planetMission.mission.locationName.ToUpper().Replace(" ", "_"));
            }
        }
    }
	
	public void Update()
    {
        if(rectTransform != null)
        {
            if(selectedMission != null)
            {
                int shift = Mathf.RoundToInt(Input.GetAxis("FormNavigate"));
                if (shift != 0 && !isLoading)
                {
                    if((guiButtonPrevious == null || !guiButtonPrevious.isAnimating) && (guiButtonNext == null || !guiButtonNext.isAnimating) && (cameraNavigator == null || !cameraNavigator.isAnimating))
                    {
                        if (!isShifting)
                        {
                            isShifting = true;
                            MissionChange(selectedMissionIndex + shift, (shift < 0) ? guiButtonPrevious : guiButtonNext);
                        }
                    }
                }
                else
                {
                    isShifting = false;
                }

                if (Input.GetButtonDown("FormAccept") || isLoading)
                {
                    MissionAccept();
                }
            }
            else
            {
                MissionChange(0);
            }

            MissionUpdate();
        }
	}

    public void ButtonPress(GUIButton button)
    {
        if (button != null)
        {
            RectTransform canvas = transform.parent.GetComponent<RectTransform>();
            button.animationAmplitude = Mathf.Abs((transform.localPosition.x + button.transform.localPosition.x) / (canvas.sizeDelta.x)) * -guiButtonPressAmplitude;
            button.Animate();
        }
    }

    public void MissionChange(int index, GUIButton linked = null)
    {
        if(planet != null)
        {
            PlanetMission[] missions = planet.GetComponentsInChildren<PlanetMission>();
            int indexRelative = (index + missions.Length) % missions.Length;
            if (missions.Length > 0)
            {
                selectedMissionIndex = indexRelative;
                selectedMission = missions[indexRelative];
            }
            
            if (cameraNavigator != null && !cameraNavigator.isAnimating)
            {
                cameraNavigator.selectedMission = selectedMission;
                cameraNavigator.Animate();
            }

            isShifting = cameraNavigator.isAnimating;

            ButtonPress(linked);
        }
    }

    public void MissionAccept()
    {
        if(isLoading)
        {
            /*
            if (guiCursor != null && !guiCursor.isFaded)
            {
                PlanetSpin planetSpin = planet.GetComponent<PlanetSpin>();
                if(planetSpin != null)
                {
                    planetSpin.rotationSpeed = 0;
                    guiCursor.Animate();
                }
            }
            */
        }
        else
        {
            ButtonPress(guiButtonLaunch);
            isLoading = true;
        }
    }

    public void MissionUpdate()
    {
        if (selectedMission != null)
        {
            if (guiTextTitle != null)
            {
                guiTextTitle.text = selectedMission.mission.locationName;
            }

            if (guiTextID != null)
            {
                guiTextID.text = string.Format(DEFAULT_GUITEXT_NUMBER_FORMAT, selectedMission.mission.id.ToString("000"));
            }

            if (guiTextDescription != null)
            {
                guiTextDescription.text = selectedMission.mission.description;
            }

            if (guiTextDifficulty != null)
            {
                guiTextDifficulty.text = new String(DEFAULT_GUITEXT_DIFFICULTY_CHAR, (int)selectedMission.mission.difficulty);
            }
        }
    }
}
