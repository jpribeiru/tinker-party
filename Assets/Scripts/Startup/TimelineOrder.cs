using UnityEngine;
using UnityEngine.Playables;

public class TimelineOrder : MonoBehaviour
{
    [SerializeField] private PlayableDirector IntroTimeline;
    [SerializeField] private PlayableDirector GreetingTimeline;
    [SerializeField] private GameObject SetupCanvas;
    private bool finalTimeline = false;
    
    void Start()
    {
        SetupCanvas.SetActive(false);
        IntroTimeline.stopped += OnIntroFinished;
        IntroTimeline.Play();

    }
    void OnIntroFinished(PlayableDirector director)
    {
        IntroTimeline.stopped -= OnIntroFinished;
        finalTimeline = true;
        GreetingTimeline.Play(); //ps timeline note, i deactivated the Introduction body before the greeting one, because for some reason, when i did it without, it wouldnt hide at the end of the 13s
    }

    bool TimelineEnded()
    {
        if (GreetingTimeline.state == PlayState.Paused && finalTimeline == true)
        {
            return true;
        }
        return false;
    }

    

    void Update()
    {
        if (TimelineEnded())
        {
            SetupCanvas.SetActive(true);
        }
        
    }


}
