using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates1Script : MonoBehaviour
{
    float swingPeriod;
    float factorMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        swingPeriod = 3f;
        factorMultiplier = 0.03f;
        LabyrinthState.AddListener(nameof(LabyrinthState.checkPoint1Passed), OnCheckPointPassed);
    }

    // Update is called once per frame
    void Update()
    {
        float factor = Time.deltaTime / swingPeriod * factorMultiplier;
        // if (!LabyrinthState.checkPoint1Passed)
        // {
        // }

        Vector3 translateDirection = factor * Vector3.down;
        Vector3 newPosition = this.transform.position + translateDirection;
        if (newPosition.y <= -0.35f || newPosition.y >= 0f)
        {
            newPosition.y = Mathf.Clamp(newPosition.y, -0.35f, 0f);
            swingPeriod = -swingPeriod;
        }
        this.transform.position = newPosition;
    }

    void OnCheckPointPassed()
    {
        if (LabyrinthState.checkPoint1Passed)
        {
            factorMultiplier = 1f;
        }
    }

    void OnDestroy()
    {
        LabyrinthState.RemoveListener(nameof(LabyrinthState.checkPoint1Passed), OnCheckPointPassed);
    }
}
