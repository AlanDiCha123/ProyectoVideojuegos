using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioSource audioSource3;
    [SerializeField] private AudioSource audioSource4;
    [SerializeField] private TextMeshProUGUI texto;

    public AudioSource Audiosource => audioSource;
    public AudioSource Audiosource2 => audioSource2;
    public AudioSource Audiosource3 => audioSource3;
    public AudioSource Audiosource4 => audioSource4;
    
    private float volumeValue = 0.3f;

    private void Start() 
    {
        audioSource.Play();
        volumeValue = PlayerPrefs.GetFloat("volume");
        audioSource.volume = volumeValue;
        slider.value = volumeValue;
        texto.SetText($"{volumeValue * 100:F0}");
    }

    private void Update() 
    {
        audioSource.volume = volumeValue;
        PlayerPrefs.SetFloat("volume", volumeValue);    
    }

    public void VariarVolumen(float value)
    {
        volumeValue = value;
        texto.SetText($"{volumeValue * 100:F0}");
    }
}