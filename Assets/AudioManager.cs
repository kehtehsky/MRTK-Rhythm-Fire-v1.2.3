using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips; 

    public AudioSource audioCom;

    public HorizontalSelector mySelector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAudio()
    {
        audioCom.Stop();
        int index = mySelector.index;
        AudioClip clip = audioClips[index];
        audioCom.clip = clip;

        if (index != 1)
        {
            audioCom.volume = 0.15f;
            audioCom.Play();
        }
        else
        {
            audioCom.volume = 1f;
            audioCom.Play();
        }
        
        

       
        
           
    }
}
