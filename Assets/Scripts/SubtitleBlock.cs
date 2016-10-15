using UnityEngine;
using System.Collections;

[System.Serializable]
public class SubtitleBlock
{
    [SerializeField]
    private string text;
    public string Text
    {
        get { return text; }
    }

    [SerializeField]
    private double startTime;
    public double StartTime
    {
        get { return startTime; }
    }

    [SerializeField]
    private double endTime;
    public double EndTime
    {
        get { return endTime; }
    }

    public bool shown;

    public SubtitleBlock(string text, double startTime, double endTime)
    {
        this.text = text;
        this.startTime = startTime;
        this.endTime = endTime;
    }
}
