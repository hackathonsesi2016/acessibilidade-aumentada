using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SubtitleManager : MonoBehaviour
{
    private const string startMessage = "Seu filme vai começar";
    private string subtitlesPath;

    [SerializeField]
    private SignVideoController signVideoController;

    [SerializeField]
    private List<string> subtitleFilenameList;

    [SerializeField]
    private MessageController messageController;

    [SerializeField]
    private Text subtitleText;    

    [SerializeField]
    public List<SubtitleBlock> subtitleBlockList;

	private double clock;
    private int currentBlock;

    private int currentSubtitleIndex = 0;

    [SerializeField]
    private bool playSubtitles = false;

    private bool waitingForNextBlock = false;

    public void ToggleSubtitlesFile()
    {
        currentSubtitleIndex = currentSubtitleIndex == subtitleFilenameList.Count - 1 ? 0 : currentSubtitleIndex + 1;

        SetSubtitleText(string.Empty);

        subtitleBlockList.Clear();

        StartSubtitleBlockCreation(subtitleFilenameList[currentSubtitleIndex]);

        FindCurrentSubtitleBlock();
    }

    private void SetSubtitleText(string text)
    {
        subtitleText.text = text;
    }

    private void ResetSubtitlePlayer()
    {
        clock = 0;
        currentBlock = 0;
        SetSubtitleText(string.Empty);
        subtitleBlockList.Clear();       
    }

    private void FindCurrentSubtitleBlock()
    {
        for (int i = 0; i < subtitleBlockList.Count; i++)
        {
            if(clock >= subtitleBlockList[i].StartTime)
            {
                continue;
            }
            else
            {
                if (clock <= subtitleBlockList[i - 1].EndTime)
                {
                    currentBlock = i - 1;
                }
                else
                    currentBlock = i;
            }
        }
    }

    private void StartSubtitleBlockCreation(string filename)
    {
        string path = subtitlesPath + filename;

        WWW subtitlesFile = new WWW(path);

        StartCoroutine(WaitSubFileToCreateSubBlocks(subtitlesFile, path));        
    }

    private void CreateSubtitleBlocks(WWW subtitlesFile, string path)
    {
        string[] blocks;

        if(string.IsNullOrEmpty(subtitlesFile.error))
        {
            blocks = SrtParser.GetSubtitleBlocks(subtitlesFile.text);

            foreach(string blockText in blocks)
            {
                double[] times = SrtParser.GetSubtitleTimes(blockText);
                string text = SrtParser.GetSubtitleText(blockText);

                SubtitleBlock block = new SubtitleBlock(text, times[0], times[1]);

                subtitleBlockList.Add(block);
            }
        }
        else
        {
            Debug.LogWarning("Subtitle not found at path: " + path);
            Debug.LogWarning(subtitlesFile.error);
        }
    }

    private IEnumerator WaitSubFileToCreateSubBlocks(WWW www, string path)
    {
        while(!www.isDone)
        {
            yield return null;
        }

        CreateSubtitleBlocks(www, path);
    }

    void Update ()
    {
        if (playSubtitles)
        {
            clock += Time.deltaTime;

            if (currentBlock < subtitleBlockList.Count)
            {
                if (clock >= subtitleBlockList[currentBlock].StartTime && !subtitleBlockList[currentBlock].shown)
                {
                    SetSubtitleText(subtitleBlockList[currentBlock].Text);

                    subtitleBlockList[currentBlock].shown = true;
                }

                if (clock >= subtitleBlockList[currentBlock].EndTime)
                {
                    SetSubtitleText(string.Empty);

                    currentBlock++;
                }
            }
            else
            {
                signVideoController.Reset(true);
            }
        }	
	}

	public void TagFound()
	{
        //clear subtitle player
        ResetSubtitlePlayer();

        //parses .srt and creates subtitle blocks
        StartSubtitleBlockCreation(subtitleFilenameList[currentSubtitleIndex]);

        //show movie start message
        messageController.ShowMessage(startMessage);        

        playSubtitles = false;        
	}

	public void TagLost()
	{
        //hide movie start message
        messageController.Hide();   

        playSubtitles = true;  
    }

    void Start()
    {

#if UNITY_EDITOR
        subtitlesPath = "file:///" + Application.streamingAssetsPath + "/";
#elif UNITY_ANDROID
        subtitlesPath = "jar:file://" + Application.dataPath + "!/assets/";
#endif

        //initializes sub block list
        subtitleBlockList = new List<SubtitleBlock>();

#if UNITY_EDITOR
        //for debugging
        //creates subtitles block without tag
        StartSubtitleBlockCreation(subtitleFilenameList[currentSubtitleIndex]);
#endif
    }
}
