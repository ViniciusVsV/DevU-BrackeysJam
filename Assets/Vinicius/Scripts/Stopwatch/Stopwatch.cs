using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    bool stopwatchActive;
    float currentTime;
    public TextMeshProUGUI currentTimeText;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        stopwatchActive = true;
        currentTime = 0;
    }

    void Update()
    {
        if (stopwatchActive)
            currentTime = currentTime + Time.deltaTime;

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");

    }

    public void StartStopwatch()
    {
        stopwatchActive = true;
    }

    public void StopStopwatch()
    {
        stopwatchActive = false;
    }

}
