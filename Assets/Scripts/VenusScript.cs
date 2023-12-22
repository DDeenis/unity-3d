using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusScript : MonoBehaviour
{
    [SerializeField]
    GameObject sun;

    GameObject surface;
    GameObject atmosphere;
    float surfaceRotationPeriod = 24.3f / 360f;
    float atmosphereRotationPeriod = 9.6f / 360f;
    float yearPeriod = 24.3f / 360f;

    // Start is called before the first frame update
    void Start()
    {
        surface = GameObject.Find("VenusSurface");
        atmosphere = GameObject.Find("VenusAtmosphere");
    }

    // Update is called once per frame
    void Update()
    {
        surface.transform.Rotate(Vector3.up, Time.deltaTime / surfaceRotationPeriod, Space.Self);
        atmosphere.transform.Rotate(Vector3.up, Time.deltaTime / atmosphereRotationPeriod);

        this.transform.RotateAround(sun.transform.position, Vector3.up, Time.deltaTime / yearPeriod);
    }
}
