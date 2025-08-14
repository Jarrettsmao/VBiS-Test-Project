    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

public class TouchAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public VolumeUI volumeUI;
    public float doubleClicktime = 0.3f;
    private bool isDragging = false;
    private float lastClickTime = 0f;
    private bool firstClick = true;
    private bool playing = false;

    public float baseScale = 1f;
    public float minScale = 1f;
    public float maxScale = 3f;

    public float scaleMultiplier = 2f;

    void Start()
    {
        transform.localScale = new Vector3(2, 2, 2);
    }

    void Update()
    {
        //check if cube hit
        DoubleClickCheck();

        DraggingCheck();
    }

    private void DoubleClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    float timeSinceLastClick = Time.time - lastClickTime;
                    // Debug.Log(timeSinceLastClick);
                    if (timeSinceLastClick <= doubleClicktime && !firstClick)
                    {
                        TogglePausePlay();
                        isDragging = false;
                    }
                    else
                    {
                        isDragging = true;
                        Vector3 worldPoint = GetMouseWorldPoint();
                        Debug.Log(worldPoint);
                        // dragOffsetY = transform.position.y - worldPoint.y;
                    }
                    lastClickTime = Time.time;
                    firstClick = false;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void DraggingCheck()
    {
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 worldPoint = GetMouseWorldPoint();
            float newY = worldPoint.y;
            float processedY = 1 - (Math.Abs(newY) / 10);

            //puts limits on how far up and down it goes
            if (newY >= -10f && newY <= 0f) //values here control the limits 
            {
                volumeUI.UpdateVolume(processedY);
                CubeSizeAndMovement(newY);
            }
        }
    }

    private void TogglePausePlay()
    {
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

    private Vector3 GetMouseWorldPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.forward, transform.position);
        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return transform.position;
    }

    private void CubeSizeAndMovement(float newY)
    {
        float processedY = 1 - (Math.Abs(newY) / 10);
        float targetScale = Mathf.Lerp(minScale, maxScale, processedY);
        transform.localScale = new Vector3(targetScale, targetScale, targetScale);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public void UpdateCubeFromVolume(float normalizedVolume)
    {
        float newY = Mathf.Lerp(-10f, 0f, normalizedVolume);
        float processedY = normalizedVolume;
        CubeSizeAndMovement(newY);
    }
}