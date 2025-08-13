using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeUI : MonoBehaviour
{
    public Slider volumeSlider;
    public TextMeshProUGUI volumeText;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        float startVolume = 0.5f;
        audioSource.volume = startVolume;

        //assign volumetext
        volumeText = volumeSlider.GetComponentInChildren<TextMeshProUGUI>();

        //ensure slider linked to volume updates
        volumeSlider.onValueChanged.AddListener(UpdateVolume);

        //update text on start
        volumeSlider.value = audioSource.volume;
        
        UpdateVolume(volumeSlider.value);
    }

    void UpdateVolume(float value)
    {
        audioSource.volume = value;

        int volumeOutOf100 = Mathf.RoundToInt(value * 100f);
        volumeText.text = $"Volume: {volumeOutOf100}";
    }
}
