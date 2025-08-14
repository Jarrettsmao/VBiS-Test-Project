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
    public TouchAudio cube;

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

    public void UpdateVolume(float value)
    {
        audioSource.volume = Mathf.Clamp01(value);

        int volumeOutOf100 = Mathf.RoundToInt(value * 100f);
        volumeText.text = $"Volume: {volumeOutOf100}";
        volumeSlider.value = audioSource.volume;

        cube.UpdateCubeFromVolume(audioSource.volume);

        //Move and scale cube based on the same value
        // if (cube != null)
        // {
        //     float newY = Mathf.Lerp(minY, maxY, value); //map 0-1 to cube Y
        //     float targetScale = ba
        // }
    }
}
