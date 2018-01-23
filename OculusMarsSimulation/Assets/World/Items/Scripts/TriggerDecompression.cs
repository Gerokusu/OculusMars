using System;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDecompression : Trigger
{
    public static readonly string DEFAULT_STRING_GUICAPTION_PERCENTAGE_FORMAT = "{0:000}%";

    public TriggerDecompressionState state = TriggerDecompressionState.Interior;
    public SettlementDoor doorIn;
    public SettlementDoor doorOut;
    public Area areaInterior;
    public Area areaDecompression;
    public Area areaExterior;
    public Text guiTextPercentage;
    public Text guiTextState;
    public Color colorInterior;
    public Color colorExterior;
    public Color colorAdapting;
    public AudioSource audioSourceDecompression;
    public AudioSource audioSourceAmbient;

    public void Start()
    {
        animatables.Add(doorIn);
        animatables.Add(areaDecompression);
        animatables.Add(doorOut);
    }

    public void Update()
    {
        if (guiTextState != null)
        {
            string value = Enum.GetName(typeof(TriggerDecompressionState), state).ToUpper();
            Color color = colorAdapting;
            if(state == TriggerDecompressionState.Interior)
            {
                color = colorInterior;
            }
            else if(state == TriggerDecompressionState.Exterior)
            {
                color = colorExterior;
            }

            guiTextState.text = value;
            guiTextState.color = color;
        }

        if (guiTextPercentage != null)
        {
            float offset = (state == TriggerDecompressionState.Interior || areaDecompression.animationType == "toexterior") ? 100 : 0;
            guiTextPercentage.text = string.Format(DEFAULT_STRING_GUICAPTION_PERCENTAGE_FORMAT, Mathf.Round(Mathf.Abs(offset - areaDecompression.animationCurrent * 100)));
        }
    }

    public override void OnActivation()
    {
        if(state == TriggerDecompressionState.Interior)
        {
            if(audioSourceDecompression != null)
            {
                audioSourceDecompression.Play();
            }

            doorIn.Animate("close", () =>
            {
                state = TriggerDecompressionState.Adapting;
                areaDecompression.Animate("toexterior", () =>
                {
                    state = TriggerDecompressionState.Exterior;
                    if(audioSourceAmbient != null)
                    {
                        audioSourceAmbient.Play();
                    }
                    doorOut.Animate("open");
                });
            });
        }
        else if(state == TriggerDecompressionState.Exterior)
        {
            if (audioSourceDecompression != null)
            {
                audioSourceDecompression.Play();
            }

            doorOut.Animate("close", () =>
            {
                state = TriggerDecompressionState.Adapting;
                areaDecompression.Animate("tointerior", () =>
                {
                    areaDecompression.animationCurrent = 0;
                    state = TriggerDecompressionState.Interior;
                    if (audioSourceAmbient != null)
                    {
                        audioSourceAmbient.Stop();
                    }
                    doorIn.Animate("open");
                });
            });
        }
    }
}
