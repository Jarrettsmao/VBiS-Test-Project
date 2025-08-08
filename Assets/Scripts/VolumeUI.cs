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
        volumeText = volumeSlider.GetComponentInChildren<TextMeshProUGUI>();
        // volumeSlider.onValueChanged.AddListener(UpdateVolume);
        // volumeSlider.value = audioSource.volume * 100.0f;
        // UpdateVolume(volumeSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
