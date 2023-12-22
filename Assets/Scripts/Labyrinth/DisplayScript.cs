using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayScript : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI clockTmp;
    [SerializeField]
    TMPro.TextMeshProUGUI scoreTmp;
    [SerializeField]
    Image image1;
    [SerializeField]
    Image image2;

    private float gameTime;
    private float score;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0f;
        score = 100f;
        LabyrinthState.AddListener(nameof(LabyrinthState.checkPoint1Amount), OnCheckPoint1StateChanged);
        LabyrinthState.AddListener(nameof(LabyrinthState.checkPoint2Amount), OnCheckPoint2StateChanged);
    }

    // Update is called once per frame
    void Update()
    {
        if (LabyrinthState.isPaused) return;
        gameTime += Time.deltaTime;
        score -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        int time = (int)gameTime;
        int hour = time / 3600;
        int minute = time % 3600 / 60;
        int second = time % 60;
        int decisecond = (int)((gameTime - time) * 10);
        clockTmp.text = $"{hour:00}:{minute:00}:{second:00}.{decisecond:0}";

        scoreTmp.text = Mathf.Ceil(score).ToString();
    }

    void OnCheckPoint1StateChanged()
    {
        image1.fillAmount = LabyrinthState.checkPoint1Amount;
    }

    void OnCheckPoint2StateChanged()
    {
        if (image2 is null) return;
        image2.fillAmount = LabyrinthState.checkPoint2Amount;
    }

    void OnDisable()
    {
        LabyrinthState.RemoveListener(nameof(LabyrinthState.checkPoint1Amount), OnCheckPoint1StateChanged);
        LabyrinthState.RemoveListener(nameof(LabyrinthState.checkPoint2Amount), OnCheckPoint2StateChanged);
    }
}
