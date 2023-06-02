using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingletonScore : MonoBehaviour
{
    private static SingletonScore instance;
    private float score;
    [SerializeField] private float taim;

    private float capturedNumberL2;

    private float copyT;

    public float Score
    {
        get { return score; }
        set { score = value; }
    }

 
    public static SingletonScore Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<SingletonScore>();
                singletonObject.name = "SingletonScore";
                DontDestroyOnLoad(singletonObject);
            }

            return instance;
        }
    }
    public void KeepingUpWithTheKardashians(float time)
    {
        copyT = time;
        
    }
    public float StopAndCaptureNumber(int nextLevel)
    {
        float finalTime = 100 - copyT;
        
        return finalTime;


    }

}

