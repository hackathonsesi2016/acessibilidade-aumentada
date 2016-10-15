using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SubtitleConfiguration : MonoBehaviour
{
    [SerializeField]
    private SubtitleManager subManager;
    [SerializeField]
    private Text subtitlesText;

    private const string joystickA = "MocuteA";
    private const string joystickB = "MocuteB";
    private const string joystickX = "MocuteX";
    private const string joystickY = "MocuteY";

    private const int minFontSize = 36;
    private const int maxFontSize = 82;

    private const int fontSizeStep = 4;

    [SerializeField]
    private List<Color> fontColors;

    [ContextMenu("Increase Font Size")]
    private void IncreaseFontSize()
    {
        subtitlesText.fontSize = Mathf.Min(subtitlesText.fontSize + fontSizeStep, maxFontSize);
    }

    [ContextMenu("Decrease Font Size")]
    private void DecreaseFontSize()
    {
        subtitlesText.fontSize = Mathf.Max(subtitlesText.fontSize - fontSizeStep, minFontSize);
    }

    [ContextMenu("Toggle Language")]
    private void ToggleLanguage()
    {
        subManager.ToggleSubtitlesFile();
    }

    [ContextMenu("Toggle Contrast")]
    private void ToggleContrast()
    {
        int currentColorIndex = fontColors.FindIndex(x => x == subtitlesText.color);

        int targetColorIndex = currentColorIndex == fontColors.Count - 1 ? 0 : currentColorIndex + 1;

        subtitlesText.color = fontColors[targetColorIndex];
    }

    private void HandleInputs()
    {
        if(Input.GetButtonUp(joystickA))
        {
            DecreaseFontSize();
        }

        if (Input.GetButtonUp(joystickB))
        {
            ToggleContrast();
        }

        if (Input.GetButtonUp(joystickX))
        {
            ToggleLanguage();
        }

        if (Input.GetButtonUp(joystickY))
        {
            IncreaseFontSize();
        }
    }

    void Start()
    {
        subtitlesText.color = fontColors[0];
    }

    void Update()
    {
        HandleInputs();
    }
}
