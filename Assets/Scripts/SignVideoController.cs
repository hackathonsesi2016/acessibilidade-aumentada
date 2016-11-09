using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class SignVideoController : MonoBehaviour
{    
    private MovieTexture movie;
    [SerializeField]
    private RawImage movieRawImage;

    private CanvasGroup movieArea;

    public void Show()
    {
        ToggleVisibility(true);
    }

    public void Hide()
    {
        ToggleVisibility(false);
    }

    void ToggleVisibility(bool show)
    {
        movieArea.alpha = show ? 1 : 0;
        movieArea.interactable = show ? true : false;
        movieArea.blocksRaycasts = show ? true : false;
    }

    public void Play()
    {
        movie.Play();
    }

    public void Pause()
    {
        movie.Pause();
    }

    public void Reset(bool hide)
    {
        movie.Stop();

        if (hide)
            Hide();
    }

    void Start()
    {
        if(movie == null)
        {
            if(movieRawImage == null)
            {
                movieRawImage = transform.GetComponentInChildren<RawImage>();                
            }

            movie = movieRawImage.texture as MovieTexture;
        }

        if(movieArea == null)
        {
            movieArea = GetComponent<CanvasGroup>();
        }
    }
}
