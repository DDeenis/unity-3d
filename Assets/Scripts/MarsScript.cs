using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarsScript : MonoBehaviour
{
    [SerializeField]
    GameObject sun;

    float dayPeriod = 24.6f / 360f;
    float yearPeriod = 68.7f / 360f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up, Time.deltaTime / dayPeriod, Space.Self);
        this.transform.RotateAround(sun.transform.position, Vector3.up, Time.deltaTime / yearPeriod);
    }
}
