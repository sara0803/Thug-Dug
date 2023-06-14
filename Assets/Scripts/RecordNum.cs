using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RecordNum : MonoBehaviour
{
    public TMPro.TextMeshProUGUI recordNumber;

    // Start is called before the first frame update
    void Start()
    {


        recordNumber.text = GetFormattedTime(PlayerPrefs.GetFloat("RecordTime", 0f));
    }

    // Update is called once per frame

    private string GetFormattedTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        print (time);
        return formattedTime;
    }
lin}
