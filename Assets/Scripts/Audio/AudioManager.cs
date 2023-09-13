using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI texto;
    
    private float volumeValue = 0.3f;

    protected override void Awake()
    {
        audioSource.volume = volumeValue;
        slider.value = volumeValue;
        PlayerPrefs.SetFloat("Volume", volumeValue);
    }

    public void VariarVolumen(float value)
    {
        texto.SetText($"{value * 100:F0}");
        audioSource.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Volume");
    }
}