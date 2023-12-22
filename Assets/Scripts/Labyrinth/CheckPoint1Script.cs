using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint1Script : MonoBehaviour
{
    float checkPoint1Timeout = 10f;

    [SerializeField]
    Image indicator;

    // Start is called before the first frame update
    void Start()
    {
        LabyrinthState.checkPoint1Amount = 1f;
        LabyrinthState.checkPoint1Passed = false;
        // indicator = GameObject.Find("CheckPoint1/Canvas/Indicator").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LabyrinthState.checkPoint1Amount > 0f)
        {
            LabyrinthState.checkPoint1Amount -= Time.deltaTime / checkPoint1Timeout;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void LateUpdate()
    {
        if (indicator.isActiveAndEnabled)
        {

            indicator.fillAmount = LabyrinthState.checkPoint1Amount;
            indicator.color = new Color(
                1f - indicator.fillAmount,
                indicator.fillAmount,
                0.3f
            );
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            LabyrinthState.checkPoint1Passed = true;
            Destroy(this.gameObject);
        }
    }
}
