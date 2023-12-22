using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate2Script : MonoBehaviour
{
    float period = 100f / 360f;
    // Start is called before the first frame update
    void Start()
    {
        LabyrinthState.AddListener(nameof(LabyrinthState.checkPoint2Passed), OnCheckPoint2Changed);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up, Time.deltaTime / period);
    }

    void OnCheckPoint2Changed()
    {
        if (LabyrinthState.checkPoint2Passed)
        {
            period /= 10;
        }
    }

    void OnDestroy()
    {
        LabyrinthState.RemoveListener(nameof(LabyrinthState.checkPoint2Passed), OnCheckPoint2Changed);
    }
}
