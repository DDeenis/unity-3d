using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject cameraAnchor;

    float camAngleX;
    float camAngleY;
    float rodAngleX;
    float rodAngleY;
    Vector3 camRod;

    // Start is called before the first frame update
    void Start()
    {
        camAngleX = this.transform.eulerAngles.x;
        camAngleY = this.transform.eulerAngles.y;
        camRod = this.transform.position;
        rodAngleX = 0f;
        rodAngleY = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        camAngleX -= my;
        camAngleY += mx;
        rodAngleX -= my;
        rodAngleY += mx;

        if (Input.GetKeyDown(KeyCode.V))
        {
            LabyrinthState.firstPersonView = !LabyrinthState.firstPersonView;
        }
    }

    void LateUpdate()
    {
        if (LabyrinthState.isPaused) return;

        Vector3 eulersAngles = new Vector3(
            Mathf.Clamp(value: camAngleX, min: 35, max: 90),
            camAngleY,
            0
        );
        Vector3 quaternionAngles = new Vector3(
            Mathf.Clamp(value: rodAngleX, min: 0, max: 89),
            rodAngleY,
            0
        );

        this.transform.eulerAngles = eulersAngles * Time.timeScale;

        if (LabyrinthState.firstPersonView)
        {
            this.transform.position = cameraAnchor.transform.position;
        }
        else
        {
            this.transform.position = Quaternion.Euler(quaternionAngles) * camRod;
        }
    }
}
