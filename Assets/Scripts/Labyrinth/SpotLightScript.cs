using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightScript : MonoBehaviour
{
    [SerializeField]
    GameObject _camera;
    Light spotLight;

    // Start is called before the first frame update
    void Start()
    {
        spotLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LabyrinthState.isPaused) return;
        if (LabyrinthState.firstPersonView && !LabyrinthState.isDay)
        {
            this.transform.position = _camera.transform.position;
            this.transform.forward = _camera.transform.forward;
            Vector2 wheel = Input.mouseScrollDelta;
            if (wheel.y != 0)
            {
                float spotAngle = Mathf.Clamp(spotLight.spotAngle + wheel.y, 25f, 90f);
                if (spotLight.spotAngle != spotAngle)
                {
                    spotLight.spotAngle = spotAngle;
                }
            }
        }
    }
}
