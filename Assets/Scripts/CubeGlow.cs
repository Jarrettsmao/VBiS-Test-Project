using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeGlow : MonoBehaviour
{
    public AudioSource audioSource;
    public Color glowColor = Color.cyan; //color of glow
    public float minEmission = 0.01f; //dim state
    public float sensitivity = 40f; // boosts loudness
    public float maxEmission = 0.75f; //bright state
    public float glowSmooth = 12f; //how fast it reacts

    private float smoothedRMS = 0f;
    private float smoothingSpeed = 5f;

    private Material cubeMaterial;
    private float[] samples = new float[64];
    private float currentEmission;

    private float baseSensitvity;

    // Start is called before the first frame update
    void Start()
    {
        cubeMaterial = GetComponent<Renderer>().material;
        currentEmission = 0f;
        cubeMaterial.DisableKeyword("_EMISSION");
        baseSensitvity = sensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource) return;

        if (!audioSource.isPlaying)
        {
            // Smoothly fade emission out
            currentEmission = Mathf.Lerp(currentEmission, 0f, Time.deltaTime * glowSmooth);
            Color changedColor = glowColor * Mathf.LinearToGammaSpace(currentEmission);
            cubeMaterial.SetColor("_EmissionColor", changedColor);

            if (currentEmission < 0.01f)
            {
                cubeMaterial.DisableKeyword("_EMISSION");
            }
            return;
        }

        cubeMaterial.EnableKeyword("_EMISSION");

        audioSource.GetOutputData(samples, 0);
        float sum = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }
        float adjustedVolume = Mathf.Pow(audioSource.volume, 0.5f);
        float rms = Mathf.Sqrt(sum / samples.Length);
        sensitivity = baseSensitvity * Mathf.Lerp(0.5f, 1.5f, audioSource.volume);

        rms *= audioSource.volume; //factor in volume to breathing

        // smoothedRMS = Mathf.Lerp(smoothedRMS, rms, Time.deltaTime * smoothingSpeed)

        //exaggerate contrast
        float threshold = 0.3f;
        float loudness = Mathf.Clamp01(rms * sensitivity - threshold) / (1 - threshold);
        loudness = Mathf.Max(minEmission, loudness);

        float targetEmission = Mathf.Lerp(minEmission, maxEmission, loudness);
        currentEmission = Mathf.Lerp(currentEmission, targetEmission, Time.deltaTime * glowSmooth);

        //Apply emission color
        Color finalColor = glowColor * Mathf.LinearToGammaSpace(currentEmission);
        cubeMaterial.SetColor("_EmissionColor", finalColor);

    }
}
