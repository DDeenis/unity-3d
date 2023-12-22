using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthScript : MonoBehaviour
{
    [SerializeField]
    GameObject sun;

    GameObject surface;
    GameObject atmosphere;
    float dayPeriod = 24f / 360f;
    float skyPeriod = 12f / 360f;
    float yearPeriod = 36.5f / 360f;

    // Start is called before the first frame update
    void Start()
    {
        surface = GameObject.Find("EarthSurface");
        atmosphere = GameObject.Find("EarthAtmosphere");
    }

    // Update is called once per frame
    void Update()
    {
        surface.transform.Rotate(Vector3.up, Time.deltaTime / dayPeriod, Space.Self);
        atmosphere.transform.Rotate(Vector3.up, Time.deltaTime / skyPeriod);

        this.transform.RotateAround(sun.transform.position, Vector3.up, Time.deltaTime / yearPeriod);
    }
}
