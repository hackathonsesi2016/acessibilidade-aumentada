using UnityEngine;
using System.Collections;
using UnityEngine.UI;
		
public class SubtitleManager : MonoBehaviour
{
    public static readonly string startMessage = "Seu filme vai começar";

	public Text subtitleText, messageText;
    public RectTransform messageArea;
	public string [] bancolegenda;

	private double clock;
	private bool playSubtitles = false;

	private void SetSubtitleText(string text)
    {
        subtitleText.text = text;
    }

    private void InitializeSubtitlePlayer()
    {
        clock = 0;
        SetSubtitleText(string.Empty);        
    }

	void Update ()
    {
        if (playSubtitles)
        {
            clock += Time.deltaTime;

            if (clock >= 0.5f && clock <= 0.1f)
            {
                SetSubtitleText(bancolegenda[0]);
            }

            if (clock >= 1.1f && clock <= 4f)
            {
                SetSubtitleText(bancolegenda[1]);
            }

            if (clock >= 5f && clock <= 9f)
            {
                SetSubtitleText(bancolegenda[2]);
            }

            if (clock >= 10f && clock <= 16f)
            {
                SetSubtitleText(bancolegenda[3]);
            }

            if (clock >= 17f && clock <= 21f)
            {
                SetSubtitleText(bancolegenda[4]);
            }

            if (clock >= 22f && clock <= 23f)
            {
                SetSubtitleText(bancolegenda[5]);
            }

            if (clock >= 23.2f && clock <= 28f)
            {
                SetSubtitleText(bancolegenda[6]);
            }

            if (clock >= 29f && clock <= 31f)
            {
                SetSubtitleText(bancolegenda[7]);
            }

            if (clock >= 32f && clock <= 36f)
            {
                SetSubtitleText(bancolegenda[8]);
            }

            if (clock >= 36.1f && clock <= 40f)
            {
                SetSubtitleText(bancolegenda[9]);
            }

            if (clock >= 42.1f && clock <= 43f)
            {
                subtitleText.text = "";
            }
        }	
	}

	public void TagFound()
	{
        //show movie start message
        messageArea.gameObject.SetActive(true);
		messageText.text = startMessage;
        
		playSubtitles = false;        
	}

	public void TagLost()
	{
        //hide movie start message
        messageArea.gameObject.SetActive(false);
        messageText.text = string.Empty;

        //initialize subtitles
        InitializeSubtitlePlayer();

        playSubtitles = true;        
    }
}
