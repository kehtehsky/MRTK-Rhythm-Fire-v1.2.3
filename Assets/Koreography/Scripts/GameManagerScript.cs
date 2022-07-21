using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using Michsky.UI.ModernUIPack;
using DG.Tweening;

public class GameManagerScript : MonoBehaviour
{
    [Header("Effect shown on target respawn")]
    public GameObject respawnParticle;

    [EventID]
    public string eventID;

    public GameObject NoteObject;

    public List<GameObject> targets;

    public EpicToonFX.ETFXTarget targetScript;

    private int score;
    public ProgressBar scoreBar;

    public CanvasGroup textToFade;







    // Local cache of the Koreography loaded into the Koreographer component.
    Koreography playingKoreo;

    [Tooltip("The Audio Source through which the Koreographed audio will be played.  Be sure to disable 'Auto Play On Awake' in the Music Player.")]
    public AudioSource audioCom;

    [Tooltip("The amount of time in seconds to provide before playback of the audio begins.  Changes to this value are not immediately handled during the lead-in phase while playing in the Editor.")]
    public float leadInTime;

    // The amount of leadInTime left before the audio is audible.
    float leadInTimeLeft;

    // The amount of time left before we should play the audio (handles Event Delay).
    float timeLeftToPlay;

    // The Sample Rate specified by the Koreography.
    public int SampleRate
    {
        get
        {
            return playingKoreo.SampleRate;
        }
    }

    // The current sample time, including any necessary delays.
    public int DelayedSampleTime
    {
        get
        {
            // Offset the time reported by Koreographer by a possible leadInTime amount.
            return playingKoreo.GetLatestSampleTime() - (int)(audioCom.pitch * leadInTimeLeft * SampleRate);
        }
    }


    private void Awake()
    {
        targetScript = NoteObject.GetComponent<EpicToonFX.ETFXTarget>();

        
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]

    // Start is called before the first frame update
    void Start()
    {
        InitializeLeadIn();

        
        Koreographer.Instance.RegisterForEvents(eventID, SpawnNote);

        score = 0;
        scoreBar.speed = 0;
        UpdateScore(0);

        Invoke("FadeText", 1);
    }

    void FadeText()
    {
        textToFade.DOFade(0, 1.5f);
    }

    void PlayAudio()
    {
        audioCom.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // Count down some of our lead-in-time.
        if (leadInTimeLeft > 0f)
        {
            leadInTimeLeft = Mathf.Max(leadInTimeLeft - Time.unscaledDeltaTime, 0f);
        }

        // Count down the time left to play, if necessary.
        if (timeLeftToPlay > 0f)
        {
            timeLeftToPlay -= Time.unscaledDeltaTime;

            // Check if it is time to begin playback.
            if (timeLeftToPlay <= 0f)
            {
                audioCom.time = -timeLeftToPlay;
                audioCom.Play();

                timeLeftToPlay = 0f;
            }
        }


    }

    public void SpawnNote(KoreographyEvent evt)
    {
        //Spawns the respawn particles
        targetScript.SpawnTarget();
        //Spawns the target object since for some reason it won't spawn the object and the particles together
        Instantiate(targets[0]);

    }

    // Sets up the lead-in-time.  Begins audio playback immediately if the specified lead-in-time is zero.
    void InitializeLeadIn()
    {
        // Initialize the lead-in-time only if one is specified.
        if (leadInTime > 0f)
        {
            // Set us up to delay the beginning of playback.
            leadInTimeLeft = leadInTime;
            timeLeftToPlay = leadInTime - Koreographer.Instance.EventDelayInSeconds;
        }
        else
        {
            // Play immediately and handle offsetting into the song.  Negative zero is the same as
            //  zero so this is not an issue.
            audioCom.time = -leadInTime;
            Invoke("PlayAudio", 3);
            //audioCom.Play();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreBar.currentPercent = score;

    }


}
