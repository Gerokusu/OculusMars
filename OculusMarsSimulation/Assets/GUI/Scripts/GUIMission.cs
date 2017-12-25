using System;
using UnityEngine;
using UnityEngine.UI;

public class GUIMission : AnimatableBehaviour
{
    public const string DEFAULT_GUITEXT_NUMBER_FORMAT = "#{0}";
    public const char DEFAULT_GUITEXT_DIFFICULTY_CHAR = '✦';
    public const string DEFAULT_GUICURSOR_CAPTION_FORMAT = "//{0}";

    public RectTransform rectTransform;
    public Text guiTextTitle;
    public Text guiTextID;
    public Text guiTextDescription;
    public Text guiTextDifficulty;
    public GUICursor guiCursor;
    public GUIButton guiButtonPrevious;
    public GUIButton guiButtonNext;
    public GUIButton guiButtonLaunch;
    public float guiButtonPressAmplitude;

    public Transform cameraTransform;
    public PlanetHub planet;
    public int selectedMissionIndex;
    public Mission selectedMission;

    private bool isShifting = false;
    private bool isLoading = false;
    private bool isFinished = false;

    public override void OnStart()
    {
        rectTransform = GetComponent<RectTransform>();
    }
	
	public override void OnUpdate()
    {
        if(rectTransform != null)
        {
            if(selectedMission != null)
            {
                int shift = Mathf.RoundToInt(Input.GetAxis("FormNavigate"));
                if (shift != 0 && !isLoading)
                {
                    if((guiButtonPrevious == null || !guiButtonPrevious.isAnimating) && (guiButtonNext == null || !guiButtonNext.isAnimating))
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
            Mission[] missions = planet.GetComponentsInChildren<Mission>();
            int indexRelative = (index + missions.Length) % missions.Length;
            if (missions.Length > 0)
            {
                selectedMissionIndex = indexRelative;
                selectedMission = missions[indexRelative];
            }

            ButtonPress(linked);
        }
    }

    public void MissionAccept()
    {
        if(isLoading)
        {
            if (guiCursor != null && !guiCursor.isFaded)
            {
                guiCursor.Animate();
                Animate();
            }
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

            if (guiCursor != null)
            {
                guiCursor.target = selectedMission.transform;
                guiCursor.guiTextCaption.text = string.Format(DEFAULT_GUICURSOR_CAPTION_FORMAT, selectedMission.locationName.ToUpper().Replace(" ", "_"));
            }
        }
    }

    public override void OnAnimationWork()
    {
        if(cameraTransform != null)
        {
            animationAcceleration -= Time.deltaTime / animationLength / 2;
            cameraTransform.localPosition = new Vector3(animationCurrent * animationAmplitude, cameraTransform.localPosition.y, cameraTransform.localPosition.z);
        }
    }

    public override void OnAnimationEnd()
    {
        isFinished = true;
    }
}
