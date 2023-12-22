using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhobosScript : MonoBehaviour
{
    [SerializeField]
    GameObject sun;
    [SerializeField]
    GameObject mars;

    float dayPeriod = 7.39f / 360f;
    float monthPeriod = 7.39f / 360f;
    float yearPeriod = 60.62f / 360f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up, Time.deltaTime / dayPeriod, Space.Self);

        this.transform.RotateAround(mars.transform.position, Vector3.up, Time.deltaTime / monthPeriod);
        this.transform.RotateAround(sun.transform.position, Vector3.up, Time.deltaTime / yearPeriod);
    }
}
