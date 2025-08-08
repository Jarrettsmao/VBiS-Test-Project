    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

public class TouchAudio : MonoBehaviour
{
    public AudioSource audioSource;
    private bool playing = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    OnCubePressed();
                }
            }
        }
    }

    private void OnCubePressed()
    {
        Debug.Log("Cube touched!");

        if (playing == false)
        {
            audioSource.Play();
            playing = true;
        }
        else
        {
            audioSource.Pause();
            playing = false;
        }    
    }
}