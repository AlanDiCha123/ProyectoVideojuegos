using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineTrigger : MonoBehaviour
{
    private PlayableDirector timeline;
    [SerializeField] private BoxCollider2D barrera;
    // Start is called before the first frame update
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
        barrera.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))    
        {
            barrera.enabled = true;
            AudioManager.Instance.Audiosource.Pause();
            AudioManager.Instance.Audiosource2.Play();
            timeline.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))    
        {
            timeline.Stop();
        }
    }
}
