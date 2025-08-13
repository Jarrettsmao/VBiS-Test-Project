using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGlow : MonoBehaviour
{
    public AudioSource audioSource
    public Color glowColor = Color.cyan; //color of glow
    public float glowSpeed; //how fast glow "breathes"
    public float glowIntensity; //how bright the glow

    private Material cubeMaterial;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        cubeMaterial = GetComponent<Renderer>().material;
        cubeMaterial.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //make cube breathe using a sine wave
            float emission = (Mathf.Sin(Time.time * glowSpeed) + 1f) / 2f;
            Color finalColor = glowColor * (emission * glowIntensity);
            cubeMaterial.SetColor("_EmissionColor", finalColor);
        }
        else
        {
            cubeMaterial.SetColor("_EmissionColor", Color.black);
        }
    }

    public void SetActiveState(bool active)
    {
        isActive = active;
    }
}
