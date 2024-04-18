using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public float time = 180f;

    // Update is called once per frame
    void Update()
    {
        //countdown
        time -= Time.deltaTime;
        //print this to the canvas
        timer.text = "Time Remaining: " + Mathf.FloorToInt(time).ToString();
    }
}
