using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    [SerializeField]
    private Text messageText;
    [SerializeField]
    private RectTransform messageArea;

    public void ShowMessage(string message)
    {
        messageArea.gameObject.SetActive(true);
        messageText.text = message;
    }

    public void Hide()
    {
        messageArea.gameObject.SetActive(false);
        messageText.text = string.Empty;
    }
}
