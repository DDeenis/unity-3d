using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    [SerializeField]
    GameObject earth;
    [SerializeField]
    GameObject sun;

    float dayPeriod = 12f / 360f;
    float monthPeriod = 12f / 360f;
    float yearPeriod = 36.5f / 360f;
    Vector3 moonAxis = Quaternion.Euler(0, 0, -30) * Vector3.up;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(moonAxis, Time.deltaTime / dayPeriod);

        this.transform.RotateAround(earth.transform.position, moonAxis, Time.deltaTime / monthPeriod);
        this.transform.RotateAround(sun.transform.position, Vector3.up, Time.deltaTime / yearPeriod);
    }
}
