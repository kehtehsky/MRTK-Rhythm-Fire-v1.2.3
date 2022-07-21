using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Michsky.UI.ModernUIPack;
using DG.Tweening;

public class SceneManagerScript : MonoBehaviour
{
    
    public AudioSource audioCom;

    public HorizontalSelector mySelector;

    public SpriteRenderer sprite;

    public ButtonManager myButton;

    public CanvasGroup playHighlight;
    public CanvasGroup nextHighlight;
    public CanvasGroup previousHighlight;

    public CanvasGroup exitHighlight;

    public CanvasGroup loadingText;

    private void Update()
    {
        
            

    }

    public void AudioPlay()
    {
        audioCom.Stop();
        audioCom.Play();
    }

    public void SwitchScenes()
    {
        int index = mySelector.index;
        SceneManager.LoadSceneAsync(index);
        loadingText.GetComponent<CanvasGroup>().alpha = 1f;
        //Invoke("DeselectPlay", 0.2f);


    }

    public void BackButton()
    {
        SceneManager.LoadSceneAsync(0);
        loadingText.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void BackHighlight()
    {
        exitHighlight.GetComponent<CanvasGroup>().alpha = 1f;
        Invoke("DeselectExit", 0.2f);
    }

    public void NextHighlight()
    {
        nextHighlight.GetComponent<CanvasGroup>().alpha = 1f;
        Invoke("DeselectPlay", 0.2f);
    }
    public void PreviousHighlight()
    {
        previousHighlight.GetComponent<CanvasGroup>().alpha = 1f;
        Invoke("DeselectPlay", 0.2f);
    }

    public void DeselectPlay()
    {
        playHighlight.GetComponent<CanvasGroup>().alpha = 0f;
        nextHighlight.GetComponent<CanvasGroup>().alpha = 0f;
        previousHighlight.GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void DeselectExit()
    {
        exitHighlight.GetComponent<CanvasGroup>().alpha = 0f;
    }
   


}
