using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint2Script : MonoBehaviour
{
    float checkPoint2Timeout = 5f;

    [SerializeField]
    Image indicator;

    // Start is called before the first frame update
    void Start()
    {
        LabyrinthState.checkPoint2Amount = 1f;
        LabyrinthState.checkPoint2Passed = false;
        // indicator = GameObject.Find("CheckPoint2/Canvas/Indicator").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LabyrinthState.checkPoint2Activated)
        {
            if (LabyrinthState.checkPoint2Amount > 0f)
            {
                LabyrinthState.checkPoint2Amount -= Time.deltaTime / checkPoint2Timeout;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void LateUpdate()
    {
        indicator.fillAmount = LabyrinthState.checkPoint2Amount;
        indicator.color = new Color(
            1f - indicator.fillAmount,
            indicator.fillAmount,
            0.3f
        );
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            LabyrinthState.checkPoint2Passed = true;
            Destroy(this.gameObject);
        }
    }
}
