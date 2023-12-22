using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    GameObject sun;
    Camera _camera;

    float cameraEulerX;
    float cameraEulerY;
    float cameraSunEulerX;
    float cameraSunEulerY;
    Vector3 cameraSun;

    private void Awake()
    {
        Debug.Log(LabyrinthState.checkPoint1Passed);
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraEulerX = this.transform.eulerAngles.x;
        cameraEulerY = this.transform.eulerAngles.y;
        cameraSunEulerX = 0;
        cameraSunEulerY = 0;
        cameraSun = sun.transform.position - this.transform.position;
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // не координаты, а данные про перемещение мыши (сдвиги)
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        cameraEulerX -= my;
        cameraEulerY += mx;

        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            cameraSunEulerX -= my;
            cameraSunEulerY += mx;
        }
    }

    private void LateUpdate()
    {
        this.transform.eulerAngles = new Vector3(cameraEulerX, cameraEulerY, 0);
        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            this.transform.position =
                sun.transform.position -
                Quaternion.Euler(cameraSunEulerX, cameraSunEulerY, 0) * cameraSun;
        }

        Vector2 scroll = Input.mouseScrollDelta;
        if (scroll != Vector2.zero)
        {
            float newValue = _camera.fieldOfView - scroll.y;
            if (newValue >= 5 && newValue <= 120)
            {
                _camera.fieldOfView = newValue;
            }
            else if (_camera.fieldOfView < 5)
            {
                _camera.fieldOfView = 5;
            }
            else if (_camera.fieldOfView > 120)
            {
                _camera.fieldOfView = 120;
            }
        }
    }
}

/*
Управление камерой:
- неидеальный подход: this.transform.rotate(-my, mx, 0)
    поворот по двум осям вызывает эффект поворота по третьей оси.
    "поворот повернутого" тела происходит по трем осям.
- более правильный подход: непосредственно устанавливать
    углы поворота (углы Эйлера), сохраняя значение 0 для z

*/
