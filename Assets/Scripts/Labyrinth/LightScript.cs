using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    bool _isDay;
    private bool isDay
    {
        get => _isDay;
        set
        {
            _isDay = value;
            LabyrinthState.isDay = value;
            if (_isDay)
            {
                SetDayLighting();
            }
            else
            {
                SetNightLighting();
            }
        }
    }

    Light lightComponent;

    // Start is called before the first frame update
    void Start()
    {
        lightComponent = GetComponent<Light>();
        isDay = true;
        LabyrinthState.isDay = isDay;
    }

    // Update is called once per frame
    void Update()
    {
        if (LabyrinthState.isPaused) return;

        if (Input.GetKeyDown(KeyCode.N))
        {
            isDay = !isDay;
        }

        if (Input.GetKeyDown(KeyCode.Equals) && lightComponent.intensity < 1f)
        {
            SetLightIntensity(lightComponent.intensity + 0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.Minus) && lightComponent.intensity > 0.01f)
        {
            SetLightIntensity(lightComponent.intensity - 0.1f);
        }
    }

    void SetDayLighting()
    {
        SetLightIntensity(1f);
    }

    void SetNightLighting()
    {
        SetLightIntensity(0.05f);
    }

    void SetLightIntensity(float intensity)
    {
        lightComponent.intensity = intensity;
        RenderSettings.skybox.SetFloat("_Exposure", intensity);
    }
}
